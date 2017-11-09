//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from SomeLanguage.g4 by ANTLR 4.7

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7")]
[System.CLSCompliant(false)]
public partial class SomeLanguageParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, ID=4, WS=5;
	public const int
		RULE_classDeclaration = 0, RULE_className = 1, RULE_method = 2, RULE_methodName = 3, 
		RULE_instruction = 4;
	public static readonly string[] ruleNames = {
		"classDeclaration", "className", "method", "methodName", "instruction"
	};

	private static readonly string[] _LiteralNames = {
		null, "'class'", "'{'", "'}'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, "ID", "WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "SomeLanguage.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static SomeLanguageParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public SomeLanguageParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public SomeLanguageParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}
	public partial class ClassDeclarationContext : ParserRuleContext {
		public ClassNameContext className() {
			return GetRuleContext<ClassNameContext>(0);
		}
		public MethodContext[] method() {
			return GetRuleContexts<MethodContext>();
		}
		public MethodContext method(int i) {
			return GetRuleContext<MethodContext>(i);
		}
		public ClassDeclarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_classDeclaration; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISomeLanguageListener typedListener = listener as ISomeLanguageListener;
			if (typedListener != null) typedListener.EnterClassDeclaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISomeLanguageListener typedListener = listener as ISomeLanguageListener;
			if (typedListener != null) typedListener.ExitClassDeclaration(this);
		}
	}

	[RuleVersion(0)]
	public ClassDeclarationContext classDeclaration() {
		ClassDeclarationContext _localctx = new ClassDeclarationContext(Context, State);
		EnterRule(_localctx, 0, RULE_classDeclaration);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 10; Match(T__0);
			State = 11; className();
			State = 12; Match(T__1);
			State = 16;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==ID) {
				{
				{
				State = 13; method();
				}
				}
				State = 18;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 19; Match(T__2);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ClassNameContext : ParserRuleContext {
		public ITerminalNode ID() { return GetToken(SomeLanguageParser.ID, 0); }
		public ClassNameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_className; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISomeLanguageListener typedListener = listener as ISomeLanguageListener;
			if (typedListener != null) typedListener.EnterClassName(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISomeLanguageListener typedListener = listener as ISomeLanguageListener;
			if (typedListener != null) typedListener.ExitClassName(this);
		}
	}

	[RuleVersion(0)]
	public ClassNameContext className() {
		ClassNameContext _localctx = new ClassNameContext(Context, State);
		EnterRule(_localctx, 2, RULE_className);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 21; Match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class MethodContext : ParserRuleContext {
		public MethodNameContext methodName() {
			return GetRuleContext<MethodNameContext>(0);
		}
		public InstructionContext[] instruction() {
			return GetRuleContexts<InstructionContext>();
		}
		public InstructionContext instruction(int i) {
			return GetRuleContext<InstructionContext>(i);
		}
		public MethodContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_method; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISomeLanguageListener typedListener = listener as ISomeLanguageListener;
			if (typedListener != null) typedListener.EnterMethod(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISomeLanguageListener typedListener = listener as ISomeLanguageListener;
			if (typedListener != null) typedListener.ExitMethod(this);
		}
	}

	[RuleVersion(0)]
	public MethodContext method() {
		MethodContext _localctx = new MethodContext(Context, State);
		EnterRule(_localctx, 4, RULE_method);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 23; methodName();
			State = 24; Match(T__1);
			State = 26;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 25; instruction();
				}
				}
				State = 28;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( _la==ID );
			State = 30; Match(T__2);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class MethodNameContext : ParserRuleContext {
		public ITerminalNode ID() { return GetToken(SomeLanguageParser.ID, 0); }
		public MethodNameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_methodName; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISomeLanguageListener typedListener = listener as ISomeLanguageListener;
			if (typedListener != null) typedListener.EnterMethodName(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISomeLanguageListener typedListener = listener as ISomeLanguageListener;
			if (typedListener != null) typedListener.ExitMethodName(this);
		}
	}

	[RuleVersion(0)]
	public MethodNameContext methodName() {
		MethodNameContext _localctx = new MethodNameContext(Context, State);
		EnterRule(_localctx, 6, RULE_methodName);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 32; Match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class InstructionContext : ParserRuleContext {
		public ITerminalNode ID() { return GetToken(SomeLanguageParser.ID, 0); }
		public InstructionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_instruction; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISomeLanguageListener typedListener = listener as ISomeLanguageListener;
			if (typedListener != null) typedListener.EnterInstruction(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISomeLanguageListener typedListener = listener as ISomeLanguageListener;
			if (typedListener != null) typedListener.ExitInstruction(this);
		}
	}

	[RuleVersion(0)]
	public InstructionContext instruction() {
		InstructionContext _localctx = new InstructionContext(Context, State);
		EnterRule(_localctx, 8, RULE_instruction);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 34; Match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x3', '\a', '\'', '\x4', '\x2', '\t', '\x2', '\x4', '\x3', 
		'\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', '\x5', '\x4', 
		'\x6', '\t', '\x6', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', 
		'\a', '\x2', '\x11', '\n', '\x2', '\f', '\x2', '\xE', '\x2', '\x14', '\v', 
		'\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x4', '\x3', '\x4', '\x3', '\x4', '\x6', '\x4', '\x1D', '\n', '\x4', 
		'\r', '\x4', '\xE', '\x4', '\x1E', '\x3', '\x4', '\x3', '\x4', '\x3', 
		'\x5', '\x3', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x2', 
		'\x2', '\a', '\x2', '\x4', '\x6', '\b', '\n', '\x2', '\x2', '\x2', '#', 
		'\x2', '\f', '\x3', '\x2', '\x2', '\x2', '\x4', '\x17', '\x3', '\x2', 
		'\x2', '\x2', '\x6', '\x19', '\x3', '\x2', '\x2', '\x2', '\b', '\"', '\x3', 
		'\x2', '\x2', '\x2', '\n', '$', '\x3', '\x2', '\x2', '\x2', '\f', '\r', 
		'\a', '\x3', '\x2', '\x2', '\r', '\xE', '\x5', '\x4', '\x3', '\x2', '\xE', 
		'\x12', '\a', '\x4', '\x2', '\x2', '\xF', '\x11', '\x5', '\x6', '\x4', 
		'\x2', '\x10', '\xF', '\x3', '\x2', '\x2', '\x2', '\x11', '\x14', '\x3', 
		'\x2', '\x2', '\x2', '\x12', '\x10', '\x3', '\x2', '\x2', '\x2', '\x12', 
		'\x13', '\x3', '\x2', '\x2', '\x2', '\x13', '\x15', '\x3', '\x2', '\x2', 
		'\x2', '\x14', '\x12', '\x3', '\x2', '\x2', '\x2', '\x15', '\x16', '\a', 
		'\x5', '\x2', '\x2', '\x16', '\x3', '\x3', '\x2', '\x2', '\x2', '\x17', 
		'\x18', '\a', '\x6', '\x2', '\x2', '\x18', '\x5', '\x3', '\x2', '\x2', 
		'\x2', '\x19', '\x1A', '\x5', '\b', '\x5', '\x2', '\x1A', '\x1C', '\a', 
		'\x4', '\x2', '\x2', '\x1B', '\x1D', '\x5', '\n', '\x6', '\x2', '\x1C', 
		'\x1B', '\x3', '\x2', '\x2', '\x2', '\x1D', '\x1E', '\x3', '\x2', '\x2', 
		'\x2', '\x1E', '\x1C', '\x3', '\x2', '\x2', '\x2', '\x1E', '\x1F', '\x3', 
		'\x2', '\x2', '\x2', '\x1F', ' ', '\x3', '\x2', '\x2', '\x2', ' ', '!', 
		'\a', '\x5', '\x2', '\x2', '!', '\a', '\x3', '\x2', '\x2', '\x2', '\"', 
		'#', '\a', '\x6', '\x2', '\x2', '#', '\t', '\x3', '\x2', '\x2', '\x2', 
		'$', '%', '\a', '\x6', '\x2', '\x2', '%', '\v', '\x3', '\x2', '\x2', '\x2', 
		'\x4', '\x12', '\x1E',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}