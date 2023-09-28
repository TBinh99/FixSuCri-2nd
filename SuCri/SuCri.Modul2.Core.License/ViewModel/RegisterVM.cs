using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Bson;
using SKM.V3.Internal;
using SKM.V3.Methods;
using SKM.V3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SuCri.Modul2.Core.License.ViewModel
{
    public partial class RegisterVM : INotifyPropertyChanged
    {
        public RegisterVM() 
        {
        
        }
        private string tokenWithAllPermission = "WyI2MTU1MzA4NiIsIkxucmJRS1JnV1EwUk94MFdNbVkvN0NzeWpabjN1T2t6UFp0YTJoY1MiXQ==";
        public ICommand RegisterCmd => new RelayCommand<Window>(Register);
        public ICommand LoginCmd => new RelayCommand<Window>(Login);
        public string Name { get; set; } = "";
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string RegisterMessage { get; set; }
        void Register(Window p) 
        {
            NotEmptyValidationRule nameValidationRule = new NotEmptyValidationRule();
            ValidationResult nameValidationResult = nameValidationRule.Validate(Name, CultureInfo.InvariantCulture);
            ValidationResult companyNameValidationResult = nameValidationRule.Validate(CompanyName, CultureInfo.InvariantCulture);

            EmailValidationRule mailValidationRule = new EmailValidationRule();
            ValidationResult mailValidationResult = mailValidationRule.Validate(Email, CultureInfo.InvariantCulture);

            if(!nameValidationResult.IsValid || !companyNameValidationResult.IsValid || !mailValidationResult.IsValid) { return; }

            GetCustomersResult resultGetCustomer = CustomerMethods.GetCustomers(tokenWithAllPermission, new GetCustomersModel()
            {
                Search = Email,
            });
            if (resultGetCustomer.Customers.Count == 0)
            {
                WTAddCustomerResult newCustomer = HelperMethods.SendRequestToWebAPI3<WTAddCustomerResult>(new AddCustomerModel()
                {
                    Name = Name,
                    CompanyName = CompanyName,
                    Email = Email,
                }, "/customer/addcustomer/", tokenWithAllPermission);

                if (newCustomer.Result == ResultType.Success)
                {
                    // Create Trial Key for each Product
                    //foreach (Product product in Enum.GetValues(typeof(Product)))
                    //{
                    //}

                    int productId = (int)Product.Sikla;

                    //Create trial Key
                    CreateKeyResult createTrialKey = SKM.V3.Methods.Key.CreateTrialKey(tokenWithAllPermission, new CreateTrialKeyModel()
                    {
                        ProductId = productId,
                        MachineCode = Helpers.GetMachineCodePI(v: 2),
                    });

                    //Activate trial key
                    WTLicenseKey.Instance.AllProductLicenseKey[(Product)productId].ActiveLicenseKey(createTrialKey.Key);
                    KeyInfoResult activateTrialKey = SKM.V3.Methods.Key.Activate(tokenWithAllPermission, new ActivateModel()
                    {
                        Key = createTrialKey.Key,
                        ProductId = productId,
                        MachineCode = Helpers.GetMachineCodePI(v: 2),
                    });

                    //Set customer for key
                    SKM.V3.Methods.Key.ChangeCustomer(tokenWithAllPermission, new ChangeCustomerModel()
                    {
                        Key = createTrialKey.Key,
                        ProductId = productId,
                        CustomerId = newCustomer.CustomerId
                    });

                    //Select Feature
                    List<Feature> FeatureActivated = new List<Feature>()
                    {
                        Feature.F1,
                        Feature.F2,
                        Feature.F3
                    };

                    //Set Feature activated for key
                    foreach (Feature feature in Enum.GetValues(typeof(Feature)))
                    {
                        if (FeatureActivated.Contains(feature))
                        {
                            SKM.V3.Methods.Key.AddFeature(tokenWithAllPermission, new FeatureModel()
                            {
                                Key = createTrialKey.Key,
                                ProductId = productId,
                                Feature = (int)feature,
                            });
                        }
                        else
                        {
                            SKM.V3.Methods.Key.RemoveFeature(tokenWithAllPermission, new FeatureModel()
                            {
                                Key = createTrialKey.Key,
                                ProductId = productId,
                                Feature = (int)feature,
                            });
                        }
                    }

                    WTLicenseKey.Instance.GetCustomerLicenses(newCustomer.Secret);

                    RegisterMessage = "You have successfully registered";

                    LicenseSingleton.Show();
                    p.Close();
                    
                }
                else 
                {
                    RegisterMessage = newCustomer.Message;
                }
            }
            else
            {
                RegisterMessage = "Email has been registered";
            }
        }
        void Login(Window p) 
        {
            LicenseSingleton.Show();
            p.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class WTAddCustomerResult : BasicResult
    {
        public int CustomerId { get; set; }
        public string Secret { get; set; }
        public string PortalLink { get; set; }
    }
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value as string))
            {
                return new ValidationResult(false, "Field cannot be empty.");
            }
            return ValidationResult.ValidResult;
        }
    }
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.ValidResult; // Allow empty value
            }

            string email = value.ToString();

            // Define a regular expression pattern for a simple email validation
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            bool isMatch = Regex.IsMatch(email, pattern);

            if (isMatch)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Enter a valid email address.");
            }
        }
    }
}
