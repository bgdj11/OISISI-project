// See https://aka.ms/new-console-template for more information
using CLI.Console;
using CLI.Model;
using CLI.DAO;
using CLI.Storage;
using CLI.Serialization;

// bojana.zoranovic@uns.ac.rs  
class Program
{
    static void Main()
    {
        MainMenu menu = new MainMenu();
        menu.RunMenu();
    }
}
