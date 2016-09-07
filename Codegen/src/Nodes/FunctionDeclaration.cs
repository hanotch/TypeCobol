﻿using System.Collections.Generic;
using TypeCobol.Compiler.CodeElements;
using TypeCobol.Compiler.CodeElements.Expressions;
using TypeCobol.Compiler.CodeElements.Functions;
using TypeCobol.Compiler.Nodes;
using TypeCobol.Compiler.Text;

namespace TypeCobol.Codegen.Nodes {

	internal class FunctionDeclaration: Compiler.Nodes.FunctionDeclaration, Generated {

		QualifiedName ProgramName = null;

		public FunctionDeclaration(Compiler.Nodes.FunctionDeclaration node): base(node.CodeElement()) {
			ProgramName = node.CodeElement().Name;
			FunctionDeclarationProfile profile = null;
			foreach(var child in node.Children) {
				if (child.CodeElement is FunctionDeclarationProfile) {
					profile = (FunctionDeclarationProfile)child.CodeElement;
					CreateOrUpdateLinkageSection(node, profile.Profile);
					var sentences = new List<Node>();
					foreach(var sentence in child.Children)
						sentences.Add(sentence);
					var pdiv = new ProcedureDivision(profile, sentences);
					children.Add(pdiv);
				} else
				if (child.CodeElement is FunctionDeclarationEnd) {
					children.Add(new ProgramEnd(ProgramName));
				} else {
					// TCRFUN_CODEGEN_NO_ADDITIONAL_DATA_SECTION
					// TCRFUN_CODEGEN_DATA_SECTION_AS_IS
					children.Add(child);
				}
			}
		}

		private void CreateOrUpdateLinkageSection(Compiler.Nodes.FunctionDeclaration node, ParametersProfile profile) {
			var linkage = node.Get<Compiler.Nodes.LinkageSection>("linkage");
			var parameters = profile.InputParameters.Count + profile.InoutParameters.Count + profile.OutputParameters.Count + (profile.ReturningParameter != null? 1:0);
			IReadOnlyList<DataDefinition> data = new List<DataDefinition>().AsReadOnly();
			if (linkage == null && parameters > 0) {
				linkage = new LinkageSection();
				children.Add(linkage);
			}
			if (linkage != null) data = linkage.Children();
			// TCRFUN_CODEGEN_PARAMETERS_ORDER
			var generated = new List<string>();
			foreach(var parameter in profile.InputParameters) {
				if (!generated.Contains(parameter.Name) && !Contains(data, parameter.Name)) {
					linkage.Add(new ParameterEntry(parameter));
					generated.Add(parameter.Name);
				}
			}
			foreach(var parameter in profile.InoutParameters) {
				if (!generated.Contains(parameter.Name) && !Contains(data, parameter.Name)) {
					linkage.Add(new ParameterEntry(parameter));
					generated.Add(parameter.Name);
				}
			}
			foreach(var parameter in profile.OutputParameters) {
				if (!generated.Contains(parameter.Name) && !Contains(data, parameter.Name)) {
					linkage.Add(new ParameterEntry(parameter));
					generated.Add(parameter.Name);
				}
			}
			if (profile.ReturningParameter != null) {
				if (!generated.Contains(profile.ReturningParameter.Name) && !Contains(data, profile.ReturningParameter.Name)) {
					linkage.Add(new ParameterEntry(profile.ReturningParameter));
					generated.Add(profile.ReturningParameter.Name);
				}
			}
		}

		private bool Contains(IReadOnlyList<DataDefinition> data, string dataname) {
			foreach(var node in data)
				if (dataname.Equals(node.Name))
					return true;
			return false;
		}

		private IList<ITextLine> _cache = null;
		public override IEnumerable<ITextLine> Lines {
			get {
				if (_cache == null) {
					_cache = new List<ITextLine>(); // TCRFUN_CODEGEN_AS_NESTED_PROGRAM
					_cache.Add(new TextLineSnapshot(-1, "IDENTIFICATION DIVISION.", null));
					_cache.Add(new TextLineSnapshot(-1, "PROGRAM-ID. "+ProgramName.Head+'.', null));
				}
				return _cache;
			}
		}

		public bool IsLeaf { get { return false; } }
	}
}
