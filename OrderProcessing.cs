using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//FIle OrdersProcessing.cs
namespace ConsoleApp {
    public class Orders {
        public string Customer { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
    public abstract class OrdersProcessing {
        private string _invoice;
        protected List<Orders> ListOrders;

        protected string Invoice {
            get {
                _invoice = "N01/2019/" + _invoice.PadLeft(4, '0');
                return _invoice;
            }
            set {
                _invoice = value.ToString();
            }
        }
        public virtual string Info() {
            var info = "List Orders : ";
            foreach (var item in ListOrders.ToArray()) {
                info += $"{item.Customer},";
            }
            return info.Remove(info.Length - 1);
        }

        public abstract bool Create(Orders item);
        public abstract Orders Read(int id);
        public abstract bool Update(int id, Orders item);
        public abstract bool Delete(int id);
    }
}
