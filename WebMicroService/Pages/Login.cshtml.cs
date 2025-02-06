using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebMicroService.ViewModels;

namespace WebMicroService.Pages
{
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class LoginModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        [BindProperty]
        public required LoginViewModel Login { get; set; }

        public required string ErrorMessage { get; set; }

        public void OnGet()
        {
            // Ewentualna inicjalizacja, np. wyzerowanie pól.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient();
            // Wywo³anie API logowania – adres dostosuj do swojego œrodowiska
            var response = await client.PostAsJsonAsync("http://restapiservice/api/Login/login", new
            {
                Login.Username,
                Login.Password
            });

            if (response.IsSuccessStatusCode)
            {
                // Logowanie udane – mo¿na przekierowaæ do chronionej strony lub dashboardu.
                return RedirectToPage("Index"); // Upewnij siê, ¿e taka strona istnieje lub zmieñ na odpowiedni¹.
            }

            ErrorMessage = "Niepoprawny login lub has³o";
            return Page();
        }
    }
}
