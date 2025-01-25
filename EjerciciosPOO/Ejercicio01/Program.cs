namespace Ejercicio01
{
    internal class Cuenta
    {
        private string titular;
        private double cantidad;

        public Cuenta (string titular, double cantidad)
        {
            this.titular = titular;
            this.cantidad = cantidad;
        }

        public Cuenta(string titular)
        {
            this.titular = titular;
            this.cantidad = 0;
        }

        public string Titular
        {
            get
            {
                return titular;
            }

            set
            {
                titular = value;
            }
        }

        public double Cantidad
        {
            get
            {
                return cantidad;
            }

            set
            {
                cantidad = value;
            }
        }

        public void Ingresar(double cantidad)
        {
            if (cantidad > 0)
            {
                this.cantidad += cantidad;
            }
        }

        public void Retirar(double cantidad)
        {
            this.cantidad -= cantidad;

            if (this.cantidad < 0)
            {
                this.cantidad = 0;
            }
        }

        public override string ToString()
        {
            return $"Titular: {this.titular}; Dinero disponible: {this.cantidad}€.";
        }

        public static void Main(string[] args)
        {
            Cuenta cuenta1 = new("Persona1");
            Cuenta cuenta2 = new("Persona2", 100);

            cuenta1.Ingresar(100);
            cuenta2.Ingresar(100);
            cuenta2.Ingresar(-50);

            cuenta1.Retirar(500);
            cuenta2.Retirar(50);

            Console.WriteLine(cuenta1.ToString());
            Console.WriteLine(cuenta2.ToString());

        }
    }
}
