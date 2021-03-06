﻿using System.Collections.Generic;
using System.Diagnostics;
using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Reflection;
using Cake.Web.Docs.Reflection.Model;
using Mono.Cecil;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents a documented fields.
    /// </summary>
    [DebuggerDisplay("{Identity,nq}")]
    public sealed class DocumentedField : DocumentedMember
    {
        /// <summary>
        /// Gets the field's identity.
        /// </summary>
        /// <value>The field's identity.</value>
        public string Identity { get; }

        /// <summary>
        /// Gets the declaring type.
        /// </summary>
        /// <value>The declaring type.</value>
        public DocumentedType Type { get; internal set; }

        /// <summary>
        /// Gets the field definition.
        /// </summary>
        /// <value>The field definition.</value>
        public FieldDefinition Definition { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentedField"/> class.
        /// </summary>
        /// <param name="info">The field info.</param>
        /// <param name="summary">The summary comment.</param>
        /// <param name="remarks">The remarks comment.</param>
        /// <param name="examples">The example comments.</param>
        /// <param name="metadata">The associated metadata.</param>
        public DocumentedField(
            IFieldInfo info,
            SummaryComment summary,
            RemarksComment remarks,
            IEnumerable<ExampleComment> examples,
            IDocumentationMetadata metadata)
            : base(MemberClassification.Type, summary, remarks, examples, metadata)
        {
            Definition = info.Definition;
            Identity = info.Identity;
        }
    }
}
