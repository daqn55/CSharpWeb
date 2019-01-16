using ChushkaPrepExam.ViewModels.Product;
using System.Collections.Generic;

namespace ChushkaPrepExam.ViewModels.Home
{
    public class LoggedInIndexViewModel
    {
        public IEnumerable<BaseProductViewModel> Products { get; set; }

        public string FullName { get; set; }
    }
}
