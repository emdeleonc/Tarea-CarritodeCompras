using System;
using System.Collections.Generic;
using System.Linq;
using CarritodeCompras.logica;

namespace CarritodeCompras
{
    class Program
    {
        static List<Producto> catalogo = new List<Producto>();
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CargarCatalogoInicial();

            Console.WriteLine("----- Carrito de compras -----\n");
            Cliente cliente = PreguntarRegistro();
            Carrito carrito = new Carrito(cliente);

            bool procesoActivo = true;
            while (procesoActivo)
            {
                MostrarCatalogo();
                Producto producto = BuscarProducto();
                if (producto == null)
                {
                    Console.WriteLine("\nProducto no encontrado, intente de nuevo\n");
                    continue; 
                }
                Console.Write("Cantidad deseada: ");
                if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
                {
                    Console.WriteLine("\nCantidad inválida. Intente de nuevo.\n");
                    continue;
                }

                bool agregado = carrito.AgregarItem(producto, cantidad);
                if (!agregado)
                {
                    Console.WriteLine("\nProducto sin stock.\n");
                    Console.WriteLine("Seguir comprando...\n");
                    continue; 
                }

                Console.WriteLine($"\n{cantidad}x{producto.Nombre} agregado(s) al carrito.\n");
                if (PreguntarSiNo("¿Desea agregar otro producto al carrito?"))
                {
                    continue;
                }
                MostrarCarrito(carrito);

                if (carrito.EstaVacio())
                {
                    Console.WriteLine("\ncarrito vacío.");
                    procesoActivo = false;
                    continue;
                }
                if (!PreguntarSiNo("\n¿Confirmar compra?"))
                {
                    Console.WriteLine();
                    continue;
                }
                bool compraExitosa = ProcesarFlujoDePago(carrito);
                if (compraExitosa)
                {
                    Console.WriteLine("\n¡Compra confirmada! Gracias por su compra.");
                    procesoActivo = false;
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

        static void CargarCatalogoInicial()
        {
            catalogo.Add(new Producto(1, "Mouse inalámbrico", 75.00, 10));
            catalogo.Add(new Producto(2, "Teclado mecánico", 250.00, 5));
            catalogo.Add(new Producto(3, "Monitor 24\"", 950.00, 3));
            catalogo.Add(new Producto(4, "Audífonos USB", 120.50, 8));
        }

        static void MostrarCatalogo()
        {
            Console.WriteLine("--- Catálogo de productos ---");
            foreach (var p in catalogo)
            {
                Console.WriteLine(p.ToString());
            }
        }

        static Producto BuscarProducto()
        {
            Console.Write("\nIngrese el ID del producto a buscar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                return null;
            }
            return catalogo.FirstOrDefault(p => p.Id == id);
        }

        static Cliente PreguntarRegistro()
        {
            if (PreguntarSiNo("¿Desea registrarse antes de comprar? (opcional)"))
            {
                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();
                Console.Write("Correo electrónico: ");
                string email = Console.ReadLine();
                Console.WriteLine();
                return new Cliente(1, nombre, email);
            }
            Console.WriteLine("\nContinuará la compra como invitado.\n");
            return new Cliente(); 
        }

        static void MostrarCarrito(Carrito carrito)
        {
            Console.WriteLine("\n--- Ver carrito ---");
            if (carrito.EstaVacio())
            {
                Console.WriteLine("El carrito está vacío.");
                return;
            }
            foreach (var item in carrito.Items)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Subtotal:   Q{carrito.CalcularSubtotal():F2}");
            Console.WriteLine($"IVA (12%):  Q{carrito.CalcularImpuestos():F2}");
            Console.WriteLine($"TOTAL:      Q{carrito.CalcularTotal():F2}");
        }

        static bool ProcesarFlujoDePago(Carrito carrito)
        {
            while (true)
            {
                Console.WriteLine("\n--- Ingresar datos de la tarjeta ---");
                Console.Write("Número de tarjeta (16 dígitos): ");
                string numero = Console.ReadLine();
                Console.Write("Nombre del titular: ");
                string titular = Console.ReadLine();
                Console.Write("Fecha de expiración (MM/AA): ");
                string fecha = Console.ReadLine();
                Console.Write("CVV: ");
                string cvv = Console.ReadLine();

                Pago pago = new Pago(numero, titular, fecha, cvv);

                if (!pago.ValidarDatos())
                {
                    Console.WriteLine("\nMostrar error en datos: revise el número, titular, fecha o CVV.");
                    if (!PreguntarSiNo("¿Desea intentar de nuevo?"))
                    {
                        return false;
                    }
                    continue;
                }

                bool aprobado = carrito.FinalizarCompra(pago);
                if (aprobado)
                {
                    Console.WriteLine($"\nPago aprobado con tarjeta {pago.NumeroEnmascarado()}.");
                    return true;
                }

                Console.WriteLine("\nPago rechazado.");
                if (!PreguntarSiNo("¿Desea intentar con otra tarjeta?"))
                {
                    return false;
                }
            }
        }

        static bool PreguntarSiNo(string pregunta)
        {
            Console.Write($"{pregunta} (s/n): ");
            string respuesta = Console.ReadLine();
            return respuesta != null && respuesta.Trim().ToLower() == "s";
        }
    }
}
