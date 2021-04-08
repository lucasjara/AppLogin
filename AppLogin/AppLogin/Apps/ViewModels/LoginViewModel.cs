using AppLogin.Apps.Helpers;
using AppLogin.Apps.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static AppLogin.Apps.Helpers.JsonSend;

namespace AppLogin.Apps.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }
        private string email;
        private string password;
        private string error;

        [Required(ErrorMessage = "Debe Ingresar el correo electrónico"),
            MaxLength(40, ErrorMessage = "El correo electrónico no puede exceder los 40 caracteres."),
            EmailAddress(ErrorMessage = "El correo electrónico enviado no es valido.")]
        public string txtEmail
        {
            get { return this.email.Trim(); }
            set { SetValue(ref this.email, value); }
        }
        [Required(ErrorMessage = "Debe Ingresar la contraseña"),
            MinLength(6, ErrorMessage = "La contraseña debe tener un minimo de 6 caracteres"),
            MaxLength(40, ErrorMessage = "La contraseña no puede exceder los 40 caracteres"),
            RegularExpression("([A-Za-z]+[0-9]|[0-9]+[A-Za-z])[A-Za-z0-9]*", ErrorMessage = "La contraseña debe tener numeros y letras.")]
        public string txtPassword
        {
            get { return this.password.Trim(); }
            set { SetValue(ref this.password, value); }
        }
        public string txtError
        {
            get { return this.error; }
            set { SetValue(ref this.error, value); }
        }

        public Command LoginCommand
        {
            get;
        }
        public Command SignUpCommand
        {
            get;
        }
        private async Task Login()
        {
            if (ValidateFields())
            {
                await SendLoginAsync(this);
            }
        }
        private async Task SendLoginAsync(LoginViewModel lvm)
        {
            /*  ----------- Envio JSON -----------
            var json = new JsonSend
            {
                header = new Header
                {
                    invokeMethod = "",
                    channel = "",
                    operationSystem = "",
                    deviceModel = "",
                    applicationVersion = "",
                    osType = 4
                },
                data = new Data
                {
                    userlogin = new Userlogin
                    {
                        email = lvm.txtEmail.Trim(),
                        password = Encryptor.MD5Hash(lvm.txtPassword.Trim())
                    }
                }
            };
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(json));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.PostAsync("http://login", httpContent);
                if (httpResponse.Content != null)
                {
                    JsonResponse jr = new JsonResponse();
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    jr = JsonConvert.DeserializeObject<JsonResponse>(responseContent);
                    int value = Convert.ToInt32(jr.msg.code);
                    switch (value)
                    {
                        case 500:
                        case 401:
                            txtError = jr.msg.msg;
                            break;
                        case 200:
                            await Application.Current.MainPage.Navigation.PushModalAsync(new Client());
                            break;
                    }
                }
            }
            ----------- Fin Envio JSON -----------
            */
            await Application.Current.MainPage.Navigation.PushModalAsync(new Client());
        }
        private bool ValidateFields()
        {
            bool flag = false;
            LoginViewModel form = this;
            ValidationContext context = new ValidationContext(form, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(form, context, validationResults, true);
            if (!valid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    txtError = validationResult.ErrorMessage;
                    break;
                }
            }
            else
            {
                txtError = "";
                flag = true;
            }
            return flag;
        }
    }
}
