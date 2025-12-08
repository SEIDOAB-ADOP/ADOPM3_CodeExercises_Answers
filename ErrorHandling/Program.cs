using System;

namespace ErrorHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ProcessUserInput();
            }
            catch (Exception ex)
            {
                AppLog.Instance.LogException(ex);
                Console.WriteLine("Pls contact our service center, you have a virus in your computer");
            }
            finally
            {
                Console.WriteLine(AppLog.Instance.WriteToDisk());
            }
        }
        private static void ProcessUserInput()
        {
            bool quit = false;
            Console.WriteLine("Don't Press Button 3, 4, 5 or 7!");
            do
            {
                Console.WriteLine("Which button do you want to press?");
                string sButtonToPress = Console.ReadLine();

                if (sButtonToPress == "q")
                {
                    quit = true;
                }
                else
                {
                    int buttonToPress;
                    if (int.TryParse(sButtonToPress, out buttonToPress))
                    {
                        try
                        {
                            PressTheButton(buttonToPress);
                            Console.WriteLine("Indeed the button was pressed successfully");
                        }
                        catch (ExplosionException ex)
                        {
                            AppLog.Instance.LogException(ex);
                            Console.WriteLine($"{ex.Message}");
                            if (ex.Severity == ErrorSeverity.fatal)
                            {
                                Console.WriteLine($"Sorry, I cannot manage this error");
                                throw;
                            }
                            Console.WriteLine($"I could manage this error, continue to play");
                        }
                        catch (InsufficientMemoryException ex)
                        {
                            AppLog.Instance.LogException(ex);
                            Console.WriteLine($"{ex.Message} - Why cant you listen!!");
                            throw;
                        }

                        finally
                        {
                            Console.WriteLine("Code here will always be executed!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, try again");
                    }
                }
            } while (!quit);
        }
        static void PressTheButton(int buttonNr)
        {
            if (buttonNr == 3)
                throw new ExplosionException("Managable KaBoom"){ ButtonPressed = buttonNr, Severity = ErrorSeverity.managable};

            if (buttonNr == 4)
                throw new ExplosionException("Fatal Crash"){ ButtonPressed = buttonNr, Severity = ErrorSeverity.fatal};

            if (buttonNr == 5)
                throw new Exception("KaBoom!!");

            if (buttonNr == 7)
                throw new InsufficientMemoryException("You hopless guy!");

            Console.WriteLine($"You pressed button {buttonNr}");
        }

    }
}

//Exercise:
//1. Create your own exception class called ExplosionException with two properties: ButtonPressed and Severity (enum with values Manageable and Fatal). 
//2. Throw a ExplosionException when button 6 is pressed with Severity set to Manageable.
//3. Throw a ExplosionException when button 8 is pressed with Severity set to Fatal.
//4. Modify the code in ProcessUserInput() to catch ExplosionException and depending on severity gives an user message(manageable) or rethrow (fatal)
