using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Private Variables

        string cacheString = "";
        List<decimal> operantList = new List<decimal>();
        List<OperationType> operatorList = new List<OperationType>();
        decimal finalResult = 0;
        int lastBracketPos = 0;

        #endregion               

        #region Suppress non-digital keys
        /// <summary>
        /// Suppress all keys except digital and operant keys 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInputText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only these keys are accepted
            if (!(("0123456789.+-*/()=".Any(c => e.KeyChar == c) || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back))))
            {
                // If the key pressed is not in above list, key press event will be canceled
                e.Handled = true;

            }

            //Key = will call the Equal button and will not show in the input text
            if (e.KeyChar == '=')
            {
                this.btnEqual_Click(null, null);
                e.Handled = true;
            }

            // Need to get focus to input textbox after pressing Tab key
            if (e.KeyChar == (char)Keys.Tab)
            {
                GetFocus();
            }
        }
        #endregion


        #region Clear Methods

        /// <summary>
        /// Clear all input text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCE_Click(object sender, EventArgs e)
        {
            this.lblResult.TextAlign = ContentAlignment.MiddleRight;
            this.UserInputText.Text = string.Empty;
            cacheString = "";
            finalResult = 0;
            this.lblResult.Text = "0";
            GetFocus();
        }

        /// <summary>
        /// Delete one character behind the current cursor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteText();
            GetFocus();
        }

        /// <summary>
        /// Delete one character in front of the current cursor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackspace_Click(object sender, EventArgs e)
        {
            BackspaceText();
            GetFocus();
        }

        #endregion


        #region Number Methods
        /// <summary>
        /// Handler methods for number keys pressed event
        /// </summary>
        /// <param name="sender">Button 0</param>
        /// <param name="e"></param>
        private void Zero_Click(object sender, EventArgs e)
        {
            InsertNumberToCache("0");
            GetFocus();
        }

        /// <summary>
        /// Handler methods for number keys pressed event
        /// </summary>
        /// <param name="sender">Button 1</param>
        /// <param name="e"></param>
        private void btnOne_Click(object sender, EventArgs e)
        {
            InsertNumberToCache("1");
            GetFocus();
        }

        /// <summary>
        /// Handler methods for number keys pressed event
        /// </summary>
        /// <param name="sender">Button 2</param>
        /// <param name="e"></param>
        private void btnTwo_Click(object sender, EventArgs e)
        {
            InsertNumberToCache("2");
            GetFocus();
        }

        /// <summary>
        /// Handler methods for number keys pressed event
        /// </summary>
        /// <param name="sender">Button 3</param>
        /// <param name="e"></param>
        private void btnThree_Click(object sender, EventArgs e)
        {
            InsertNumberToCache("3");
            GetFocus();
        }

        /// <summary>
        /// Handler methods for number keys pressed event
        /// </summary>
        /// <param name="sender">Button 4</param>
        /// <param name="e"></param>
        private void btnFour_Click(object sender, EventArgs e)
        {
            InsertNumberToCache("4");
            GetFocus();
        }

        /// <summary>
        /// Handler methods for number keys pressed event
        /// </summary>
        /// <param name="sender">Button 5</param>
        /// <param name="e"></param>
        private void btnFive_Click(object sender, EventArgs e)
        {
            InsertNumberToCache("5");
            GetFocus();
        }

        /// <summary>
        /// Handler methods for number keys pressed event
        /// </summary>
        /// <param name="sender">Button 6</param>
        /// <param name="e"></param>
        private void btnSix_Click(object sender, EventArgs e)
        {
            InsertNumberToCache("6");
            GetFocus();
        }

        /// <summary>
        /// Handler methods for number keys pressed event
        /// </summary>
        /// <param name="sender">Button 7</param>
        /// <param name="e"></param>
        private void btnSeven_Click(object sender, EventArgs e)
        {
            InsertNumberToCache("7");
            GetFocus();
        }

        /// <summary>
        /// Handler methods for number keys pressed event
        /// </summary>
        /// <param name="sender">Button 8</param>
        /// <param name="e"></param>
        private void btnEight_Click(object sender, EventArgs e)
        {
            InsertNumberToCache("8");
            GetFocus();
        }

        /// <summary>
        /// Handler methods for number keys pressed event
        /// </summary>
        /// <param name="sender">Button 9</param>
        /// <param name="e"></param>
        private void btnNine_Click(object sender, EventArgs e)
        {
            InsertNumberToCache("9");
            GetFocus();
        }

        /// <summary>
        /// Handler methods for Dot keys pressed event
        /// </summary>
        /// <param name="sender">Dot key</param>
        /// <param name="e"></param>
        private void btnDot_Click(object sender, EventArgs e)
        {
            InsertNumberToCache(".");
            GetFocus();
        }

        /// <summary>
        /// Flick the sign of the number in front of curent cursor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNegative_Click(object sender, EventArgs e)
        {
            FlickSign();
            GetFocus();
        }


        #endregion


        #region Operator Methods
        /// <summary>
        /// Handler method for Multiply operator       
        /// </summary>
        /// <param name="sender">Multiply button</param>
        /// <param name="e"></param>
        /// 
        private void btnMultiply_Click(object sender, EventArgs e)
        {
            InsertCommand("*");
            GetFocus();
        }

        /// <summary>
        /// Handler method for Divide operator       
        /// </summary>
        /// <param name="sender">Divide button</param>
        /// <param name="e"></param>
        /// 
        private void btnDevide_Click(object sender, EventArgs e)
        {
            InsertCommand("/");
            GetFocus();
        }

        /// <summary>
        /// Handler method for Plus operator       
        /// </summary>
        /// <param name="sender">Plus button</param>
        /// <param name="e"></param>
        /// 
        private void BtnPlus_Click(object sender, EventArgs e)
        {
            InsertCommand("+");
            GetFocus();
        }

        /// <summary>
        /// Handler method for Minus operator       
        /// </summary>
        /// <param name="sender">Minus button</param>
        /// <param name="e"></param>
        /// 
        private void btnMinus_Click(object sender, EventArgs e)
        {
            InsertCommand("-");
            GetFocus();
        }

        /// <summary>
        /// Handler method for first round bracket       
        /// </summary>
        /// <param name="sender">First round bracket  button</param>
        /// <param name="e"></param>
        /// 
        private void btnFirstRB_Click(object sender, EventArgs e)
        {
            InsertCommand("(");
            GetFocus();
        }

        /// <summary>
        /// Handler method for second round bracket       
        /// </summary>
        /// <param name="sender">Second round bracket  button</param>
        /// <param name="e"></param>
        /// 
        private void btnSB_Click(object sender, EventArgs e)
        {
            InsertCommand(")");
            GetFocus();
        }

        #endregion


        #region Calculator Methods

        private void btnSquare_Click(object sender, EventArgs e)
        {
            #region Add square command in line of current equation and process it later

            InsertCommand("^2");

            #endregion

            #region Press x2 to process on the last result

            //ProcessCalculation(UserInputText.Text);

            //finalResult = finalResult * finalResult;
            //this.lblResult.TextAlign = ContentAlignment.MiddleRight;
            //lblResult.Text = ((double)finalResult).ToString();
            #endregion

            GetFocus();
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            #region Add SQRT command in line of current equation and process it later

            InsertCommand("Sqrt(");

            #endregion

            #region Press SQRT() to process on the last result

            //ProcessCalculation(UserInputText.Text);

            //this.lblResult.TextAlign = ContentAlignment.MiddleRight;
            //if (finalResult >= 0)
            //{

            //    finalResult = (decimal)Math.Sqrt((double)finalResult);
            //    lblResult.Text = finalResult.ToString();
            //}
            //else
            //{

            //    //this.lblResult.TextAlign = ContentAlignment.TopLeft;
            //    lblResult.Text = "ERROR...";
            //}
            #endregion
            GetFocus();
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            this.lblResult.TextAlign = ContentAlignment.MiddleRight;
            ProcessCalculation(UserInputText.Text);
            GetFocus();
        }

        #endregion        


        #region Helper Methods

        private void FlickSign()
        {
            // 1111+1112223-4*435
            // Save current cursor position
            if (cacheString.Length == 0)
                return;
            var currentSelectionStart = this.UserInputText.TextLength - cacheString.Length;

            // There is no number in infront of current curosr
            if (currentSelectionStart <= 0)
            {
                cacheString = (decimal.Parse(cacheString) * (-1)).ToString();
                this.UserInputText.Text = cacheString;
                this.UserInputText.SelectionStart = this.UserInputText.TextLength;
                this.UserInputText.SelectionLength = 0;
                return;
            }

            // The sign of the number is +
            if (this.UserInputText.Text.Substring(currentSelectionStart - 1, 1) == "+")
            {
                // Delete the + then add -
                this.UserInputText.Text = this.UserInputText.Text.Remove(currentSelectionStart - 1, 1).Insert(currentSelectionStart - 1, "-");
                //cacheString = (decimal.Parse(cacheString) * (-1)).ToString();
            }
            // The sign of the number is -
            else if (this.UserInputText.Text.Substring(currentSelectionStart - 1, 1) == "-")
            {
                // Delete the - then add +
                this.UserInputText.Text = this.UserInputText.Text.Remove(currentSelectionStart - 1, 1).Insert(currentSelectionStart - 1, "+");
            }
            else
            {
                // Just add -
                this.UserInputText.Text = this.UserInputText.Text.Insert(currentSelectionStart, "-");
            }


            this.UserInputText.SelectionStart = this.UserInputText.TextLength;
            this.UserInputText.SelectionLength = 0;
        }

        private void InsertNumberToCache(string value)
        {
            // Save current cursor position
            var currentSelectionStart = this.UserInputText.SelectionStart;

            this.UserInputText.Text = this.UserInputText.Text.Insert(currentSelectionStart, value);
            this.UserInputText.SelectionStart = currentSelectionStart + value.Length;

            this.UserInputText.SelectionLength = 0;

            // Save to cache memory
            cacheString += value;

        }

        private void InsertCommand(string value)
        {
            // Save current cursor position
            var currentSelectionStart = this.UserInputText.SelectionStart;

            this.UserInputText.Text = this.UserInputText.Text.Insert(currentSelectionStart, value);
            this.UserInputText.SelectionStart = currentSelectionStart + value.Length;

            this.UserInputText.SelectionLength = 0;

            // Clear cache memory
            cacheString = "";

        }

        private void DeleteText()
        {
            if (this.UserInputText.TextLength < this.UserInputText.SelectionStart + 1)
                return;

            // Save current cursor position
            var currentSelectionStart = this.UserInputText.SelectionStart;

            this.UserInputText.Text = this.UserInputText.Text.Remove(currentSelectionStart, this.UserInputText.SelectionLength > 0 ? this.UserInputText.SelectionLength : 1); ;
            this.UserInputText.SelectionStart = currentSelectionStart - this.UserInputText.SelectionLength;

            this.UserInputText.SelectionLength = 0;
        }

        private void BackspaceText()
        {
            // Save current cursor position
            var currentSelectionStart = this.UserInputText.SelectionStart;

            if (this.UserInputText.TextLength <= 0 || currentSelectionStart == 0)
                return;



            if (this.UserInputText.SelectionLength > 0)
            {
                this.UserInputText.Text = this.UserInputText.Text.Remove(currentSelectionStart, this.UserInputText.SelectionLength);
                this.UserInputText.SelectionStart = currentSelectionStart - this.UserInputText.SelectionLength - this.UserInputText.SelectionLength;
            }
            else
            {
                this.UserInputText.Text = this.UserInputText.Text.Remove(currentSelectionStart - 1, 1);
                this.UserInputText.SelectionStart = currentSelectionStart - this.UserInputText.SelectionLength - 1;
            }

            this.UserInputText.SelectionLength = 0;
        }

        private void GetFocus()
        {
            this.UserInputText.Focus();
        }

        private decimal CalculateEquation()
        {
            try
            {
                // var input = inputString.Replace(" ", "");

                string s = "-1+2+3*(55555+44)*20";

                while (operatorList.Count > 0)
                {

                    if (operatorList.BinarySearch(OperationType.Multiply) >= 0)
                    {
                        // Get the index of the Multiply operator
                        var indx = operatorList.BinarySearch(OperationType.Multiply);

                        // Process Multiply 
                        var tempRes = operantList[indx] * operantList[indx + 1];

                        // Remove that Multiply operator in the operator list
                        operatorList.RemoveAt(indx);

                        // Replace the 2 operants with the result
                        operantList.RemoveAt(indx + 1);
                        operantList.RemoveAt(indx);
                        operantList.Insert(indx, tempRes);
                    }
                    else if (operatorList.BinarySearch(OperationType.Divide) >= 0)
                    {
                        // Get the index of the Divide operator
                        var indx = operatorList.BinarySearch(OperationType.Divide);

                        // Process Divide 
                        var tempRes = operantList[indx] / operantList[indx + 1];

                        // Remove that Divide operator in the operator list
                        operatorList.RemoveAt(indx);

                        // Replace the 2 operants with the result
                        operantList.RemoveAt(indx + 1);
                        operantList.RemoveAt(indx);
                        operantList.Insert(indx, tempRes);
                    }
                    else if (operatorList.BinarySearch(OperationType.Plus) >= 0)
                    {
                        // Get the index of the Plus operator
                        var indx = operatorList.BinarySearch(OperationType.Plus);

                        // Process Plus 
                        var tempRes = operantList[indx] + operantList[indx + 1];

                        // Remove that Plus operator in the operator list
                        operatorList.RemoveAt(indx);

                        // Replace the 2 operants with the result
                        operantList.RemoveAt(indx + 1);
                        operantList.RemoveAt(indx);
                        operantList.Insert(indx, tempRes);
                    }
                    else if (operatorList.BinarySearch(OperationType.Minus) >= 0)
                    {
                        // Get the index of the Minus operator
                        var indx = operatorList.BinarySearch(OperationType.Minus);

                        // Process Minus 
                        var tempRes = operantList[indx] - operantList[indx + 1];

                        // Remove that Minus operator in the operator list
                        operatorList.RemoveAt(indx);

                        // Replace the 2 operants with the result
                        operantList.RemoveAt(indx + 1);
                        operantList.RemoveAt(indx);
                        operantList.Insert(indx, tempRes);
                    }

                }

                finalResult = operantList[0];

                return finalResult;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Information");
            }

            return 0;

        }

        private void ParseSimpleOperation(string inputString)
        {

            decimal operant = 0;
            int currPos = 0;
            string temp;

            operantList.Clear();
            operatorList.Clear();


            for (int i = 0; i < inputString.Length; i++)
            {
                if ("+-*/".Any(c => c == inputString[i]))
                {
                    // Found an operator in the middle of the input string
                    if (i > currPos)
                    {
                        // Cut the number in front of the operator
                        temp = inputString.Substring(currPos, i - currPos);

                        // Convert to interger and save to a list. If not successful then throw an exception
                        if (!decimal.TryParse(temp, out operant))
                        {
                            if (temp.IndexOf('^') > 0)
                            {
                                temp = temp.Substring(0, temp.Length - 2);

                                double num;
                                if (!double.TryParse(temp, out num))
                                    throw new InvalidOperationException($"Invalid operant: {temp} is not a number");

                                operant = (decimal)Math.Pow(2, 2);
                            }
                            else
                            {
                                operant = 0;
                                throw new InvalidOperationException($"Invalid operant: {temp} is not a number");
                            }
                        }
                        // Add the operant in the left to the list
                        operantList.Add(operant);

                        // Add the operator to the list
                        switch (inputString[i])
                        {
                            case '*':
                                operatorList.Add(OperationType.Multiply); break;

                            case '/':
                                operatorList.Add(OperationType.Divide); break;
                            case '+':
                                operatorList.Add(OperationType.Plus); break;
                            case '-':
                                operatorList.Add(OperationType.Minus); break;
                            default:
                                throw new InvalidOperationException($"Invalid operantor: {inputString[i]}");

                        }

                        // Save the last position of operator
                        currPos = i + 1;

                    }
                    // Found an operator at the begining of equation. Only accept Minus and Plus operator
                    else
                    {
                        if (!(inputString[i] == '+' || inputString[i] == '-'))
                            throw new InvalidOperationException($"Invalid operantion: Missing an operant befor the {inputString[i]} operator");
                    }


                }

            }

            // If the last string is an operator, throw an exception
            if ("+-*/".Any(c => c == inputString[inputString.Length - 1]))
                throw new InvalidOperationException("Need an operator after the last operator!");

            // Cut the last number
            temp = inputString.Substring(currPos, inputString.Length - currPos);

            // Convert to interger and save to a list. If not successful then throw an exception
            if (!decimal.TryParse(temp, out operant))
            {
                if (temp.IndexOf('^') > 0)
                {
                    temp = temp.Substring(0, temp.Length - 2);

                    double num;
                    if (!double.TryParse(temp, out num))
                        throw new InvalidOperationException($"Invalid operant: {temp} is not a number");

                    operant = (decimal)Math.Pow(2, 2);
                }
                else
                {
                    operant = 0;
                    throw new InvalidOperationException($"Invalid operant: {temp} is not a number");
                }
            }
            operantList.Add(operant);
        }

        /// <summary>
        /// Find the parameter string of the SQRT function
        /// </summary>
        /// <param name="inputString">Input string that inclue the SQRT function</param>
        /// <param name="outputString">Parameter string found</param>
        /// <param name="position">Position of the last round bracket of the parameter string</param>
        private void FindParameterString_Sqrt(string inputString, out string outputString, out int position)
        {
            //Assign default value for output parameter
            outputString = "";
            position = -1;

            try
            {
                // Find the first ( bracket of the last SQRT() function
                int fstBracketIdx = inputString.LastIndexOf('t') + 1;

                // Find the corresponding ( bracket to the previous ) bracket position:                   

                // Number of bracket couple () inside the SQRT() function
                int bracketCoupleNo = 1;

                // Cut the string from the position of the SQRT function
                string evaluateString = inputString.Substring(fstBracketIdx + 1);

                for (int i = 0; i < evaluateString.Length; i++)
                {
                    // If find a ( bracket, increase the bracket couple counter
                    if (evaluateString[i] == '(')
                    {
                        bracketCoupleNo++;
                    }
                    // If find a ) bracket, decrease the bracket couple counter
                    else if (evaluateString[i] == ')')
                    {
                        bracketCoupleNo--;
                    }
                    // If found the corresponding ) bracket of the SQRT() function
                    if (bracketCoupleNo == 0)
                    {
                        // Cut the string/equation that belong to SQRT() function
                        outputString = evaluateString.Substring(0, i);
                        position = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }


        }

        private decimal CalculateSqrt(string inputString)
        {
            decimal subResult = 0;
            string paraString;

            // Find the parameter string of this SQRT() function
            FindParameterString_Sqrt(inputString.Substring(3), out paraString, out lastBracketPos);

            //Parameter value that under the SQRT function
            decimal paraNumber = 0;
            // Check if it is number or an equation
            if (decimal.TryParse(paraString, out paraNumber))
            {
                // Calculate the SQRT() function
                subResult = (decimal)Math.Sqrt((double)paraNumber);

            }
            // This is new equation that need to calculate 
            else
            {
                // Parse this sub-equation
                ParseSimpleOperation(paraString);

                // Calculate this sub-equation
                subResult = CalculateEquation();

                // Calculate the SQRT() function
                subResult = (decimal)Math.Sqrt((double)subResult);

            }

            return subResult;
        }

        private void ProcessCalculation(string inputString)
        {
            string s = "(-1+(2+3)*(55555+(823-34)*44)*20))";

            //s = "-1+2+3*1*20";
            //s = "2+3";

            // If there is no input and user press Equal button then return 0
            if (inputString.Length == 0)
            {
                finalResult = 0;
                lblResult.Text = "0";
                return;
            }

            try
            {
                // Get the original input string and process on it
                string equation = inputString;

                // Validate the number of ( and ) bracket and add the ) bracket if needed or issue an error if it not valid
                if (equation.LastIndexOf('(') > -1)
                {
                    // Get the difference of ( and ) brackets
                    var count = equation.Count(c => c == '(') - equation.Count(c => c == ')');

                    // If the ( bracket is more than ) bracket, need to add more ) bracket to the end of the equation
                    if (count > 0)
                    {
                        //add more ) bracket to the end of the equation
                        while (count > 0)
                        {
                            equation += ')';
                            count--;
                        }
                    }
                    // Too many ) bracket then it should be an error
                    else if (count < 0)
                    {
                        throw new InvalidOperationException($"Invalid operant: Too many ) braket");
                    }
                }

                // Check if there is any SQRT() function then calculate it from right side to left side
                while (equation.LastIndexOf('t') > -1)
                {
                    s = "1+(2-3)*sqrt(4+ sqrt(2*(2+3)) ) /5+ ((1+ sqrt(2) )^2 -5)";

                    // Save the beginning position of SQRT() function
                    int sqrtPos = equation.LastIndexOf('S');

                    // Calculate the SQRT function
                    var subResult = CalculateSqrt(equation.Substring(equation.LastIndexOf('S')));

                    // Add the result to the original equation string
                    equation = equation.Remove(sqrtPos, 6 + lastBracketPos).Insert(sqrtPos, subResult.ToString());

                }


                // Process equations in round bracket one by one until there is no in-round-bracket equation
                while (equation.LastIndexOf('(') > -1)
                {
                    // Find the first ) bracket
                    int secBracketIdx = equation.IndexOf(')');

                    // Find the corresponding ( bracket to the previous ) bracket
                    int fstBracketIdx = equation.Substring(0, secBracketIdx).LastIndexOf('(');

                    // Cut the substring within the round bracket ()
                    string subEquation = equation.Substring(fstBracketIdx + 1, secBracketIdx - fstBracketIdx - 1);

                    // Parse this sub-equation
                    ParseSimpleOperation(subEquation);

                    // Calculate this sub-equation
                    var subResult = CalculateEquation();

                    // Replace the sub-equation with its results
                    equation = equation.Replace("(" + subEquation + ")", subResult.ToString());
                }

                // Final equation without any round bracket
                //Just process it
                ParseSimpleOperation(equation);

                // Calculate this sub-equation
                finalResult = CalculateEquation();

                // Show result to GUI
                lblResult.Text = finalResult.ToString();

            }
            catch (Exception ex)
            {
                this.lblResult.TextAlign = ContentAlignment.TopLeft;
                lblResult.Text = ex.ToString();
                //MessageBox.Show(ex.ToString(), "Exception Information");
            }

        }


        #endregion       


    }
}
