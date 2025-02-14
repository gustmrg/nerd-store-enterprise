using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Models;

public class UserRegisterInputModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Nome completo")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "CPF")]
    [Cpf]
    public string DocumentNumber { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres"), MinLength(8)]
    public string Password { get; set; }
    
    [DisplayName("Confirme sua senha")]
    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    public string ConfirmPassword { get; set; }
}