﻿using System.Web;
using System.Web.Mvc;
using Cake.Web.Core;
using Cake.Web.Core.Rendering;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection;

namespace Cake.Web.Helpers.Api
{
    public static class MethodHelper
    {
        private static MethodRenderOption FixOptions(DocumentedMethod method, MethodRenderOption options)
        {
            if (method.Metadata.IsAlias)
            {
                if (method.Metadata.IsPropertyAlias)
                {
                    options |= MethodRenderOption.PropertyAlias;
                }
                else
                {
                    options |= MethodRenderOption.MethodAlias;
                }
            }
            return options;
        }

        public static IHtmlString MethodName(this ApiServices context, DocumentedMethod method)
        {
            if (method != null)
            {
                var options = MethodRenderOption.Name | MethodRenderOption.Parameters;
                options = FixOptions(method, options);

                var signature = context.SignatureResolver.GetMethodSignature(method);
                return context.SignatureRenderer.Render(signature, options);
            }
            return MvcHtmlString.Empty;
        }

        public static IHtmlString MethodName(this ApiServices context, DocumentedMethod method, MethodRenderOption options)
        {
            if (method != null)
            {
                options = FixOptions(method, options);
                var signature = context.SignatureResolver.GetMethodSignature(method);
                return context.SignatureRenderer.Render(signature, options);
            }
            return MvcHtmlString.Empty;
        }

        public static IHtmlString MethodLink(this ApiServices context, DocumentedMethod method)
        {
            return MethodLink(context, method, MethodRenderOption.Name | MethodRenderOption.Parameters);
        }

        public static IHtmlString MethodLink(this ApiServices context, DocumentedMethod method, MethodRenderOption options)
        {
            if (method != null)
            {
                options = FixOptions(method, options);
                var signature = context.SignatureResolver.GetMethodSignature(method);
                return context.SignatureRenderer.Render(signature, MethodRenderOption.Link | options);
            }
            return MvcHtmlString.Empty;
        }

        public static IHtmlString ExtensionMethodLink(this ApiServices context, DocumentedMethod method)
        {
            return MethodLink(context, method, MethodRenderOption.Name | MethodRenderOption.Parameters | MethodRenderOption.ExtensionMethod);
        }

        public static IHtmlString MethodClassificationName(this ApiServices context, DocumentedMethod type)
        {
            if (type != null)
            {
                if (type.Metadata.IsAlias)
                {
                    if (type.Metadata.IsPropertyAlias)
                    {
                        return MvcHtmlString.Create("Cake Property Alias");
                    }
                    return MvcHtmlString.Create("Cake Method Alias");
                }

                switch (type.MethodClassification)
                {
                    case MethodClassification.Constructor:
                        return MvcHtmlString.Create("Constructor");
                    case MethodClassification.EventAccessor:
                        return MvcHtmlString.Create("Event");
                    case MethodClassification.ExtensionMethod:
                        return MvcHtmlString.Create("Extension Method");
                    case MethodClassification.Method:
                        return MvcHtmlString.Create("Method");
                    case MethodClassification.Operator:
                        return MvcHtmlString.Create("Operator");
                    default:
                        return MvcHtmlString.Create("Unknown");
                }
            }
            return MvcHtmlString.Create(string.Empty);
        }
    }
}