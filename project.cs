
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF               =  0, // (EOF)
        SYMBOL_ERROR             =  1, // (Error)
        SYMBOL_WHITESPACE        =  2, // Whitespace
        SYMBOL_MINUS             =  3, // '-'
        SYMBOL_LPAREN            =  4, // '('
        SYMBOL_RPAREN            =  5, // ')'
        SYMBOL_TIMES             =  6, // '*'
        SYMBOL_COMMA             =  7, // ','
        SYMBOL_DIV               =  8, // '/'
        SYMBOL_COLON             =  9, // ':'
        SYMBOL_LBRACE            = 10, // '{'
        SYMBOL_RBRACE            = 11, // '}'
        SYMBOL_PLUS              = 12, // '+'
        SYMBOL_LT                = 13, // '<'
        SYMBOL_EQ                = 14, // '='
        SYMBOL_GT                = 15, // '>'
        SYMBOL_COLLECTION        = 16, // collection
        SYMBOL_DEF               = 17, // def
        SYMBOL_IDENTIFIER        = 18, // Identifier
        SYMBOL_IF                = 19, // if
        SYMBOL_INTEGER           = 20, // integer
        SYMBOL_NUMBER            = 21, // Number
        SYMBOL_OTHER             = 22, // other
        SYMBOL_PHRASE            = 23, // phrase
        SYMBOL_PRECISE           = 24, // precise
        SYMBOL_RANGE             = 25, // range
        SYMBOL_REPEAT            = 26, // repeat
        SYMBOL_STRINGLITERAL     = 27, // StringLiteral
        SYMBOL_TRUTH             = 28, // truth
        SYMBOL_UNTIL             = 29, // until
        SYMBOL_ADDITIVE          = 30, // <additive>
        SYMBOL_ARGUMENTS         = 31, // <arguments>
        SYMBOL_ASSIGNMENT        = 32, // <assignment>
        SYMBOL_BLOCK             = 33, // <block>
        SYMBOL_CONDITION         = 34, // <condition>
        SYMBOL_DECLARATION       = 35, // <declaration>
        SYMBOL_EXPRESSION        = 36, // <expression>
        SYMBOL_FACTOR            = 37, // <factor>
        SYMBOL_LOOP              = 38, // <loop>
        SYMBOL_METHODCALL        = 39, // <methodCall>
        SYMBOL_METHODDECLARATION = 40, // <methodDeclaration>
        SYMBOL_MULTIPLICATIVE    = 41, // <multiplicative>
        SYMBOL_PARAM             = 42, // <param>
        SYMBOL_PARAMS            = 43, // <params>
        SYMBOL_PROGRAM           = 44, // <Program>
        SYMBOL_STATEMENT         = 45, // <statement>
        SYMBOL_STATEMENTS        = 46, // <statements>
        SYMBOL_TYPE              = 47  // <type>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM                                              =  0, // <Program> ::= <statements>
        RULE_STATEMENTS                                           =  1, // <statements> ::= <statement>
        RULE_STATEMENTS2                                          =  2, // <statements> ::= <statements> <statement>
        RULE_STATEMENT                                            =  3, // <statement> ::= <assignment>
        RULE_STATEMENT2                                           =  4, // <statement> ::= <declaration>
        RULE_STATEMENT3                                           =  5, // <statement> ::= <condition>
        RULE_STATEMENT4                                           =  6, // <statement> ::= <loop>
        RULE_STATEMENT5                                           =  7, // <statement> ::= <methodDeclaration>
        RULE_STATEMENT6                                           =  8, // <statement> ::= <methodCall>
        RULE_DECLARATION_IDENTIFIER_EQ                            =  9, // <declaration> ::= <type> Identifier '=' <expression>
        RULE_TYPE_INTEGER                                         = 10, // <type> ::= integer
        RULE_TYPE_PRECISE                                         = 11, // <type> ::= precise
        RULE_TYPE_PHRASE                                          = 12, // <type> ::= phrase
        RULE_TYPE_TRUTH                                           = 13, // <type> ::= truth
        RULE_TYPE_COLLECTION_LT_GT                                = 14, // <type> ::= collection '<' <type> '>'
        RULE_ASSIGNMENT_IDENTIFIER_EQ                             = 15, // <assignment> ::= Identifier '=' <expression>
        RULE_CONDITION_IF_LPAREN_RPAREN                           = 16, // <condition> ::= if '(' <expression> ')' <block>
        RULE_CONDITION_IF_LPAREN_RPAREN_OTHER                     = 17, // <condition> ::= if '(' <expression> ')' <block> other <block>
        RULE_BLOCK_LBRACE_RBRACE                                  = 18, // <block> ::= '{' <statements> '}'
        RULE_LOOP_REPEAT_UNTIL_LPAREN_RPAREN                      = 19, // <loop> ::= repeat <block> until '(' <expression> ')'
        RULE_LOOP_RANGE_LPAREN_COMMA_RPAREN                       = 20, // <loop> ::= range '(' <expression> ',' <expression> ')' <block>
        RULE_METHODDECLARATION_DEF_IDENTIFIER_LPAREN_RPAREN_COLON = 21, // <methodDeclaration> ::= def Identifier '(' <params> ')' ':' <block>
        RULE_PARAMS                                               = 22, // <params> ::= <param>
        RULE_PARAMS_COMMA                                         = 23, // <params> ::= <params> ',' <param>
        RULE_PARAM_IDENTIFIER_COLON                               = 24, // <param> ::= Identifier ':' <type>
        RULE_METHODCALL_IDENTIFIER_LPAREN_RPAREN                  = 25, // <methodCall> ::= Identifier '(' <arguments> ')'
        RULE_ARGUMENTS                                            = 26, // <arguments> ::= <expression>
        RULE_ARGUMENTS_COMMA                                      = 27, // <arguments> ::= <arguments> ',' <expression>
        RULE_EXPRESSION                                           = 28, // <expression> ::= <additive>
        RULE_ADDITIVE_PLUS                                        = 29, // <additive> ::= <additive> '+' <multiplicative>
        RULE_ADDITIVE_MINUS                                       = 30, // <additive> ::= <additive> '-' <multiplicative>
        RULE_ADDITIVE                                             = 31, // <additive> ::= <multiplicative>
        RULE_MULTIPLICATIVE_TIMES                                 = 32, // <multiplicative> ::= <multiplicative> '*' <factor>
        RULE_MULTIPLICATIVE_DIV                                   = 33, // <multiplicative> ::= <multiplicative> '/' <factor>
        RULE_MULTIPLICATIVE                                       = 34, // <multiplicative> ::= <factor>
        RULE_FACTOR_IDENTIFIER                                    = 35, // <factor> ::= Identifier
        RULE_FACTOR_NUMBER                                        = 36, // <factor> ::= Number
        RULE_FACTOR_STRINGLITERAL                                 = 37, // <factor> ::= StringLiteral
        RULE_FACTOR_LPAREN_RPAREN                                 = 38, // <factor> ::= '(' <expression> ')'
        RULE_FACTOR_MINUS                                         = 39  // <factor> ::= '-' <factor>
    };

    public class MyParser
    {
        private LALRParser parser;

        public MyParser(string filename)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLLECTION :
                //collection
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEF :
                //def
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //Identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTEGER :
                //integer
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUMBER :
                //Number
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OTHER :
                //other
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PHRASE :
                //phrase
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRECISE :
                //precise
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RANGE :
                //range
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_REPEAT :
                //repeat
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL :
                //StringLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TRUTH :
                //truth
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_UNTIL :
                //until
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ADDITIVE :
                //<additive>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARGUMENTS :
                //<arguments>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENT :
                //<assignment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BLOCK :
                //<block>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONDITION :
                //<condition>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECLARATION :
                //<declaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOOP :
                //<loop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODCALL :
                //<methodCall>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODDECLARATION :
                //<methodDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MULTIPLICATIVE :
                //<multiplicative>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAM :
                //<param>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMS :
                //<params>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<Program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTS :
                //<statements>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPE :
                //<type>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM :
                //<Program> ::= <statements>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTS :
                //<statements> ::= <statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTS2 :
                //<statements> ::= <statements> <statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<statement> ::= <assignment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT2 :
                //<statement> ::= <declaration>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT3 :
                //<statement> ::= <condition>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT4 :
                //<statement> ::= <loop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT5 :
                //<statement> ::= <methodDeclaration>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT6 :
                //<statement> ::= <methodCall>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_IDENTIFIER_EQ :
                //<declaration> ::= <type> Identifier '=' <expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_INTEGER :
                //<type> ::= integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_PRECISE :
                //<type> ::= precise
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_PHRASE :
                //<type> ::= phrase
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_TRUTH :
                //<type> ::= truth
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_COLLECTION_LT_GT :
                //<type> ::= collection '<' <type> '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_IDENTIFIER_EQ :
                //<assignment> ::= Identifier '=' <expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONDITION_IF_LPAREN_RPAREN :
                //<condition> ::= if '(' <expression> ')' <block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONDITION_IF_LPAREN_RPAREN_OTHER :
                //<condition> ::= if '(' <expression> ')' <block> other <block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BLOCK_LBRACE_RBRACE :
                //<block> ::= '{' <statements> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOOP_REPEAT_UNTIL_LPAREN_RPAREN :
                //<loop> ::= repeat <block> until '(' <expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOOP_RANGE_LPAREN_COMMA_RPAREN :
                //<loop> ::= range '(' <expression> ',' <expression> ')' <block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODDECLARATION_DEF_IDENTIFIER_LPAREN_RPAREN_COLON :
                //<methodDeclaration> ::= def Identifier '(' <params> ')' ':' <block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMS :
                //<params> ::= <param>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMS_COMMA :
                //<params> ::= <params> ',' <param>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAM_IDENTIFIER_COLON :
                //<param> ::= Identifier ':' <type>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODCALL_IDENTIFIER_LPAREN_RPAREN :
                //<methodCall> ::= Identifier '(' <arguments> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTS :
                //<arguments> ::= <expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTS_COMMA :
                //<arguments> ::= <arguments> ',' <expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<expression> ::= <additive>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDITIVE_PLUS :
                //<additive> ::= <additive> '+' <multiplicative>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDITIVE_MINUS :
                //<additive> ::= <additive> '-' <multiplicative>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDITIVE :
                //<additive> ::= <multiplicative>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTIPLICATIVE_TIMES :
                //<multiplicative> ::= <multiplicative> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTIPLICATIVE_DIV :
                //<multiplicative> ::= <multiplicative> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTIPLICATIVE :
                //<multiplicative> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_IDENTIFIER :
                //<factor> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_NUMBER :
                //<factor> ::= Number
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_STRINGLITERAL :
                //<factor> ::= StringLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_LPAREN_RPAREN :
                //<factor> ::= '(' <expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_MINUS :
                //<factor> ::= '-' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            //todo: Report message to UI?
        }

    }
}
