using System;
using System.Linq;
using PV178.Homeworks.HW02.Machine;
using PV178.Homeworks.HW02.Machine.ControlUnit;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02
{
    public class Program
    {
        #region CommandConstants
        
        /// <summary>
        /// Usage: "list"
        /// </summary>
        private const string ListAllProductsCommand = "list";

        /// <summary>
        /// Usage: "insert 50"
        /// </summary>
        private const string InsertCoinCommand = "insert";

        /// <summary>
        /// Usage: "select 3 D"
        /// </summary>
        private const string SelectCoordinatesCommand = "select";

        /// <summary>
        /// Usage: "confirm"
        /// </summary>
        private const string ConfirmOrderCommand = "confirm";

        /// <summary>
        /// Usage: "help"
        /// </summary>
        private const string HelpCommand = "help";

        /// <summary>
        /// Usage: "list"
        /// </summary>
        private const string ExitCommand = "exit"; 
        
        #endregion

        private static IVendingMachine vendingMachine;

        static void Main(string[] args)
        {
            vendingMachine = new Machine.VendingMachine(new ControlUnit(
                                                        new[] { 0, 1, 2, 3, 4, 5, 6, 7 },
                                                        new[] { 'A', 'B', 'C', 'D', 'E', 'F' }));

            ModelInitializer.Initialize(vendingMachine.ControlUnit);

            Console.WriteLine("Welcome to the Machine.");
            for (;;)
            {
                Console.WriteLine("Type in command:" + Environment.NewLine);
                var input = Console.ReadLine();
                AnalyzeInput(input);
            }
        }

        #region CommandProcessing

        private static void AnalyzeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }
            var lowerCaseInput = input.ToLower();

            if (lowerCaseInput.Contains(ListAllProductsCommand))
            {
                ProcessListAllProductsCommand();
                return;
            }
            if (lowerCaseInput.Contains(InsertCoinCommand))
            {
                ProcessInsertCommand(lowerCaseInput);
                return;
            }
            if (lowerCaseInput.Contains(SelectCoordinatesCommand))
            {
                ProcessSelectCommand(lowerCaseInput);
                return;
            }
            if (lowerCaseInput.Contains(ConfirmOrderCommand))
            {
                ProcessConfirmCommand();
                return;
            }
            if (lowerCaseInput.Contains(HelpCommand))
            {
                ProcessHelpCommand();
                return;
            }
            if (lowerCaseInput.Contains(ExitCommand))
            {
                Environment.Exit(0);
            }
            Console.WriteLine($"Command '{input}' was not recognized, type 'help' to see valid commands." + Environment.NewLine);
        }

        private static void ProcessListAllProductsCommand()
        {
            var products = vendingMachine.GetCurrentOffer();
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();
        }

        private static void ProcessInsertCommand(string lowerCaseInput)
        {
            var insertCommandData = lowerCaseInput.Split(' ');
            decimal value;
            if (insertCommandData.Length != 2 || !decimal.TryParse(insertCommandData[1], out value))
            {
                Console.WriteLine($"Parameter '{insertCommandData.Last()}' is not correct, type 'help' to see valid examples." + Environment.NewLine);
                return;
            }
            vendingMachine.InsertCoin(value);
            Console.WriteLine();
        }

        private static void ProcessSelectCommand(string lowerCaseInput)
        {
            var selectCommandData = lowerCaseInput.Split(' ');
            int rowIndex;
            if (selectCommandData.Length != 3 ||
                selectCommandData[2].Length == 0 ||
                !int.TryParse(selectCommandData[1], out rowIndex))
            {
                Console.WriteLine($"Parameters '{lowerCaseInput.Replace("select", string.Empty)}' for select command are not correct, type 'help' to see valid examples." + Environment.NewLine);
                return;
            }
            vendingMachine.SelectCoordinates(new Coordinates(rowIndex, char.ToUpper(selectCommandData[2][0])));
            Console.WriteLine();
        }

        private static void ProcessConfirmCommand()
        {
            vendingMachine.ConfirmOrder();
            Console.WriteLine();
        }

        private static void ProcessHelpCommand()
        {
            Console.WriteLine("Supported commands:" + Environment.NewLine);
            Console.WriteLine("'list' - lists all available products");
            Console.WriteLine("'insert 50' - insert coin which has value 50");
            Console.WriteLine("'select 3 D' - choose coordinates 3 D");
            Console.WriteLine("'confirm' - confirm given order");
            Console.WriteLine("'exit' - quit console application");
            Console.WriteLine();
        } 
        
        #endregion
    }
}
