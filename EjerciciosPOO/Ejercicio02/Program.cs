namespace Ejercicio02
{
    internal class Persona
    {
        private string nombre = "";
        private int edad = 0;
        private string dni;
        private char sexo = 'H';
        private double peso = 0;
        private double altura = 0;

        public string Nombre 
        {
            set 
            { 
                nombre = value;
            }
        }

        public int Edad
        {
            set
            {
                edad = value;
            }
        }

        public char Sexo
        {
            set
            {
                sexo = value;
            }
        }

        public double Peso
        {
            set
            {
                peso = value;
            }
        }

        public double Altura
        {
            set
            {
                altura = value;
            }
        }

        public Persona()
        {

        }

        public Persona(string nombre, int edad, char sexo)
        {
            this.nombre = nombre;
            this.edad = edad;
            this.sexo = sexo;
        }

        public Persona(string nombre, int edad, string dni, char sexo, double peso, double altura)
        {
            this.nombre = nombre;
            this.edad = edad;
            this.dni = dni;
            this.sexo = sexo;
            this.peso = peso;
            this.altura = altura;
        }




        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
