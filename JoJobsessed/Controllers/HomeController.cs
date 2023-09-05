using JoJobsessed.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.Json;

namespace JoJobsessed.Controllers
{
    public class HomeController : Controller
    {
        private Clothes_Shop_DatabaseContext _databaseContext;

        public HomeController(Clothes_Shop_DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult autorization()
        {
            if (HttpContext.Session.Keys.Contains("AuthUser"))
            {
                return RedirectToAction("main");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> autorization(Login login)
        {
            if (ModelState.IsValid)
            {
                User user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.LoginUser == login.LoginUser && u.PasswordUser == login.PasswordUser);
                if (user != null)
                {
                    HttpContext.Session.SetString("AuthUser", login.LoginUser);
                    await Auth(login.LoginUser);
                    return RedirectToAction("autorization");
                }
            }
            return View();
        }

        private async Task Auth(string UserName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, UserName)
            };
            ClaimsIdentity IdUser = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(IdUser));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("AuthUser");
            return RedirectToAction("autorization");
        }

        public IActionResult registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> registration(User user)
        {
            if (ModelState.IsValid)
            {
                _databaseContext.Users.Add(user);
                await _databaseContext.SaveChangesAsync();
                return RedirectToAction("main");
            }
            else
            {
                return View();
            }
        }

        public IActionResult lostpassword ()
        {
            return View();
        }

        public IActionResult confirmpassword()
        {
            return View();
        }

        public IActionResult newpassword()
        {
            return View();
        }

        public IActionResult main()
        {
            return View();
        }

        public async Task<IActionResult> catalog()
        {
            return View(await _databaseContext.Products.ToListAsync());
        }

        public IActionResult cart()
        {
            Cart cart = new Cart();
            if (HttpContext.Session.Keys.Contains("Cart"))
                cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart"));
            return View(cart);
        }

        public IActionResult AddToCart()
        {
            int ID = Convert.ToInt32(Request.Query["ID"]);
            Cart cart = new Cart();
            if (HttpContext.Session.Keys.Contains("Cart")) 
                cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart"));
            cart.CartLines.Add(_databaseContext.Products.Find(ID));
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize<Cart>(cart));
            return Redirect("~/Home/catalog");
        }

        public IActionResult tech_support()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SendMail(Send send)
        {
            MailAddress mailAddressFrom = new MailAddress("sergey_5_rubley@mail.ru", "JoJobsessed");
            MailAddress mailAddressTo = new MailAddress(send.EMail);
            MailMessage mailMessagee = new MailMessage(mailAddressFrom, mailAddressTo);
            mailMessagee.Subject = "обращение от пользователя";
            mailMessagee.Body = send.EMail;
            mailMessagee.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("smpt.mail.ru", 587);
            smtpClient.Credentials = new NetworkCredential("sergey_5_rubley@mail.ru", "003g5uGrTtPmw9Xeevhb");
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessagee);

            MailAddress mailAddressToo = new MailAddress("sergey_5_rubley@mail.ru", "JoJobsessed");
            MailMessage mailMessageToo = new MailMessage(mailAddressToo, mailAddressToo);
            mailMessageToo.Subject = "обращение от пользователя " + send.EMail;
            mailMessageToo.Body = send.Message;
            mailMessageToo.IsBodyHtml = true;

            smtpClient.Credentials = new NetworkCredential("sergey_5_rubley@mail.ru", "003g5uGrTtPmw9Xeevhb");
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessageToo);
           
            return View("tech_support");
        }
    }
}