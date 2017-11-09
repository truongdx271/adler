using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntlrTestCsharp.domain;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime;

namespace AntlrTestCsharp.parser
{
    public class ListenerOrientedParser : Parser
    {
        public ClassObject parse(string code)
        {
            AntlrInputStream ips = new AntlrInputStream(code);
            SomeLanguageLexer lexer = new SomeLanguageLexer(ips);
            UnbufferedTokenStream tokens = new UnbufferedTokenStream(lexer);
            SomeLanguageParser parser = new SomeLanguageParser(tokens);

            ClassListener classListener = new ClassListener();
            parser.classDeclaration().EnterRule(classListener);
            return classListener.getParsedClass();
        }

        class ClassListener : SomeLanguageBaseListener
        {
            private ClassObject parsedClass;

            public override void EnterClassDeclaration([NotNull] SomeLanguageParser.ClassDeclarationContext context)
            {
                string className = context.className().GetText();
                MethodListener methodListener = new MethodListener();
                foreach(var method in context.method())
                {
                    method.EnterRule(methodListener);
                }
                List<Method> methods = methodListener.getMethods();
                parsedClass = new ClassObject(className, methods);
            }

            public ClassObject getParsedClass()
            {
                return parsedClass;
            }
        }

        class MethodListener : SomeLanguageBaseListener
        {
            private List<Method> methods;

            public MethodListener()
            {
                methods = new List<Method>();
            }

            public override void EnterMethod([NotNull] SomeLanguageParser.MethodContext context)
            {
                string methodName = context.methodName().GetText();
                InstructionListener instructionlistener = new InstructionListener();
                foreach(var instruction in context.instruction())
                {
                    instruction.EnterRule(instructionlistener);
                }
                List<Instruction> instructions = instructionlistener.getInstructions();
                methods.Add(new Method(methodName, instructions));
            }

            public List<Method> getMethods()
            {
                return methods;
            }
        }

        class InstructionListener : SomeLanguageBaseListener
        {
            private List<Instruction> instructions;

            public InstructionListener()
            {
                instructions = new List<Instruction>();
            }

            
            public override void EnterInstruction([NotNull] SomeLanguageParser.InstructionContext context)
            {
                string instructionName = context.GetText();
                instructions.Add(new Instruction(instructionName));
            }

            public List<Instruction> getInstructions()
            {
                return instructions;
            }

        }
    }
}
