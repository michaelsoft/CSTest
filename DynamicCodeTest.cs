using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CSharp;
using System;
using System.IO;

public class DynamicCodeTest
{

    public static void Test()
    {
        CSharpCodeProvider codeProvider = new CSharpCodeProvider();
        ICodeCompiler icc = codeProvider.CreateCompiler();
        string Output = "Out.exe";

        var path = @"C:\Test\CSTest\DynamicCode.txt";
        var srcStr = System.IO.File.ReadAllText(path);

        var msg = "";
        System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
        //Make sure we generate an EXE, not a DLL
        parameters.GenerateExecutable = true;
        parameters.OutputAssembly = Output;
        CompilerResults results = icc.CompileAssemblyFromSource(parameters, srcStr);

        if (results.Errors.Count > 0)
        {
            foreach (CompilerError CompErr in results.Errors)
            {
                msg = msg +
                            "Line number " + CompErr.Line +
                            ", Error Number: " + CompErr.ErrorNumber +
                            ", '" + CompErr.ErrorText + ";" +
                            Environment.NewLine + Environment.NewLine;
            }
        }
        else
        {
            //Successful Compile
            msg = "Success!";
            //If we clicked run then launch our EXE
            Process.Start(Output);
        }
    }

}

