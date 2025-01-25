
using PruebaDAM;


int[] numeros = [1, 2, 3, -2, -5];
string text = "hola mundo";

Console.WriteLine(numeros.Max());
Console.WriteLine(numeros.Average());
Console.WriteLine(numeros.Select(n => n * 2).Max());
Console.WriteLine(numeros.Select(Pow).Max());

Console.WriteLine(numeros.Where(n => n > 2).First());
Console.WriteLine(numeros.OrderByDescending(n => n).First());
Console.WriteLine(numeros.OrderByDescending(n => n > 0 ? -1 : 1).First());
Console.WriteLine(numeros.OrderByDescending(n => -n).First());

Console.WriteLine(numeros.OrderBy(n => -n).First());

Console.WriteLine(new string(text.SkipWhile(c => c != 'a').ToArray()));
Console.WriteLine(new string(text.Reverse().ToArray()));

int Pow(int num)
{
    return num * num;
}




byte bait = 150; // 0 - 255 (8 bits)
int entero32bits = 45484;
long entero64bits = 51661116161;
float decimal32bits = 3453.4547f;
double decimal64bits = 6116161.615616;
decimal decimal128bits = 611611616.6116611m;

char caracter = 'a';
string texto = "hola mundo";
string concatenacion = texto + caracter + entero32bits;
string interpolacion = $"Hola {caracter}";
string especiales = @"C:\Users\Luma\Documents\Programación\Grado\Segundo Año";

bool boleano = true;

object objeto = caracter + "15" + " " + texto + " " + 100;

Action<int, bool> action = HacerAlgo;
Func<int, bool, string> function = HacerAlgo2;

action(56, true);

Console.WriteLine(objeto);
string a = Console.ReadLine();

if (boleano) { 
} 
else { 
}

char primer = texto[0];

foreach (char c in texto) { 
    
}

int[] misNumeros = [ 1, 2, 3 ];
List<int> misNumeros2 = [1, 2, 3];
List<object> misNumeros3 = [1, "", true];

void HacerAlgo(int num, bool a) { 

}

string HacerAlgo2(int num, bool a)
{
    return "";
}


Prueba pruebaClase = new Prueba();
int abs = pruebaClase.Atributo;
Console.WriteLine(pruebaClase.Atributo);
pruebaClase.Atributo = 8;
Console.WriteLine(pruebaClase.Atributo);

class Persona(int age)
{
    public int age;
}


public class A { }