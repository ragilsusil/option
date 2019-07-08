using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// File Program.cs
namespace ConsoleApp {
    public class Menu {
        public Menu(IEnumerable<string> items) {
            Items = items.ToArray();
        }


        public IReadOnlyList<string> Items { get; }

        public int SelectedIndex { get; private set; } = -1; // nothing selected

        public string SelectedOption => SelectedIndex != -1 ? Items[SelectedIndex] : null;


        public void MoveUp() => SelectedIndex = Math.Max(SelectedIndex - 1, 0);

        public void MoveDown() => SelectedIndex = Math.Min(SelectedIndex + 1, Items.Count - 1);
    }


    // logic for drawing menu list
    public class ConsoleMenuPainter {
        readonly Menu menu;

        public ConsoleMenuPainter(Menu menu) {
            this.menu = menu;
        }

        public void Paint(int x, int y) {
            for (int i = 0; i < menu.Items.Count; i++) {
                Console.SetCursorPosition(x, y + i);

                var color = menu.SelectedIndex == i ? ConsoleColor.Yellow : ConsoleColor.Gray;

                Console.ForegroundColor = color;
                Console.WriteLine(menu.Items[i]);
            }
        }
    }

    public interface IProgram {
        void Info();
    }
    public class Program : IProgram {

        public void Info() {
            
        }
        public static void Main(string[] args) {
            var provider = CultureInfo.CreateSpecificCulture("id-ID");
            var product = new ProductProcessing<Product>();
            var panganan = new Product();
            var menu = new Menu(new string[] { "1. Create", "2. Read", "3. Update", "4. Delete", "5. Exit" });
            var menuPainter = new ConsoleMenuPainter(menu);
            IProgram program = new Program();
            product.Index();
        home:
            


            bool done = false;

            do {
                menuPainter.Paint(0, 0);

                var keyInfo = Console.ReadKey();

                switch (keyInfo.Key) {
                    case ConsoleKey.UpArrow: menu.MoveUp(); break;
                    case ConsoleKey.DownArrow: menu.MoveDown(); break;
                    case ConsoleKey.Enter: done = true; break;
                }
            }
            while (!done);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Selected option: " + (menu.SelectedOption ?? "(nothing)"));
            
            if (menu.SelectedOption == menu.Items[0]) {
                Console.Clear();
                Console.Write("MAsukan ID = ");
                int id = Convert.ToInt16(Console.ReadLine());
                Console.Write("MAsukan Nama Jenis (Makanan/Minuman) = ");
                string jenis = Convert.ToString(Console.ReadLine());
                Console.Write("MAsukan Nama Brand = ");
                string brand = Convert.ToString(Console.ReadLine());
                Console.Write("MAsukan Barcode = ");
                string barc = Convert.ToString(Console.ReadLine());
                Console.Write("MAsukan Description = ");
                string desc = Convert.ToString(Console.ReadLine());
                Console.Write("MAsukan harga = ");
                double rego = Convert.ToDouble(Console.ReadLine());
                panganan = new Product() {
                    Id = id,
                    Name = jenis,
                    Brand = brand,
                    Barcode = barc,
                    Description = desc,
                    Price = rego,
                };
                product.Create(panganan);
                Console.WriteLine();
                Console.WriteLine("data terinput!!!");
                Console.WriteLine("enter to continue");
                Console.ReadKey();
                Console.Clear();
                goto home;
            } else if (menu.SelectedOption == menu.Items[1]) {
                Console.Clear();
                Console.WriteLine("Menu Tampilkan Data");
                Console.WriteLine("--------------------------------------");
                foreach (Product item in product.Read().ToArray()) {
                    Console.WriteLine($"Dish        : {item.Name} - {item.Brand}");
                    Console.WriteLine($"Description : {item.Description}");
                    Console.WriteLine($"Price (IDR) : {item.Price.ToString("C2", provider)}");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();
                }
                Console.WriteLine("pres enter untuk lanjut !!!");
                Console.ReadKey();
                Console.Clear();
                goto home;
            } else if (menu.SelectedOption == menu.Items[2]) {
                Console.WriteLine("Update:");
                Console.Write("MAsukan ID = ");
                var Id = Int32.Parse(Console.ReadLine());
                Console.Write("MAsukan Nama Jenis (Makanan/Minuman) = ");
                panganan.Name = Convert.ToString(Console.ReadLine());
                Console.Write("MAsukan Nama Brand = ");
                panganan.Brand = Convert.ToString(Console.ReadLine());
                Console.Write("MAsukan Barcode = ");
                panganan.Barcode = Convert.ToString(Console.ReadLine());
                Console.Write("MAsukan Description = ");
                panganan.Description = Convert.ToString(Console.ReadLine());
                Console.Write("MAsukan harga = ");
                panganan.Price = Convert.ToDouble(Console.ReadLine());
                product.Update(Id, panganan);
                Console.WriteLine("pres enter untuk lanjut !!!");
                Console.ReadKey();
                Console.Clear();
                goto home;
            } else if (menu.SelectedOption == menu.Items[3]) {
                Console.WriteLine("MENU HAPUS DATA");
                Console.Write("Masukan ID yang akan Anda Hapus : ");
                var id = Int32.Parse(Console.ReadLine());
                product.Delete(id);
                Console.WriteLine("pres enter untuk lanjut !!!");
                Console.ReadKey();
                Console.Clear();
                goto home;
            } else if (menu.SelectedOption == menu.Items[4]) {
                Console.Clear();
                Console.WriteLine("Terimakasih !!!");
                System.Environment.Exit(1);
                Console.Clear();
            } else {
                Console.WriteLine("Inputan Anda Salah !!!");
                Console.WriteLine("tekan enter untuk lanjut....");
                Console.ReadKey();
                Console.Clear();
                goto home;
            }
            int pil = Convert.ToInt16(Console.ReadLine());
            Console.ReadKey();
        }
    }
}
