using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

public class IndexModel : PageModel
{
    public class InsuranceCalculatorModel
    {
        public decimal VehicleValue { get; set; }
        public int ClaimCountLastYear { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;  // Establecer la fecha por defecto a la actual

        public string CalculateBaseValue()
        {
            decimal baseValue = VehicleValue * 0.05m;
            return baseValue.ToString("C0"); // Mostrar en formato de moneda sin decimales
        }

        public string CalculateIncrement()
        {
            decimal increment = 0m;

            switch (ClaimCountLastYear)
            {
                case 1:
                    increment = CalculateBaseValueAsDecimal() * 0.05m;
                    break;
                case 2:
                    increment = CalculateBaseValueAsDecimal() * 0.15m;
                    break;
                case 3:
                    increment = CalculateBaseValueAsDecimal() * 0.30m;
                    break;
                case 4:
                default:
                    increment = CalculateBaseValueAsDecimal() * 0.50m;
                    break;
            }

            return increment.ToString("C0"); // Mostrar en formato de moneda sin decimales
        }

        private decimal CalculateBaseValueAsDecimal()
        {
            return VehicleValue * 0.05m;
        }

        public string CalculateTotalBeforeTax()
        {
            decimal totalBeforeTax = CalculateBaseValueAsDecimal() + decimal.Parse(CalculateIncrement(), System.Globalization.NumberStyles.Currency);
            return totalBeforeTax.ToString("C0"); // Mostrar en formato de moneda sin decimales
        }

        public string CalculateTotalAfterTax()
        {
            decimal totalBeforeTax = decimal.Parse(CalculateTotalBeforeTax(), System.Globalization.NumberStyles.Currency);
            decimal ivaPercentage = 0.19m;

            decimal totalAfterTax = totalBeforeTax * (1 + ivaPercentage);
            return totalAfterTax.ToString("C0"); // Mostrar en formato de moneda sin decimales
        }
    }

    [BindProperty]
    public InsuranceCalculatorModel CalculatorModel { get; set; }

    public void OnGet()
    {
        CalculatorModel = new InsuranceCalculatorModel();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            return Page();
        }

        return Page();
    }
}
