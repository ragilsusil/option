using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp {
    public interface IProductProcessing {
        void Index();
        bool Create(Product item);
        Product Read(int id);
        List<Product> Read();
        bool Update(int id, Product item);
        bool Delete(int id);
        string Info();
        void Dispose();
    }
}
