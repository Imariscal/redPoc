using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedPoc.Entity.Viewmodels
{
    public class OrderViewModelList : BaseViewModel
    { 
        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }
}
