﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aspire.Cli.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class PublishCommandStrings {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PublishCommandStrings() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Aspire.Cli.Resources.PublishCommandStrings", typeof(PublishCommandStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string Description {
            get {
                return ResourceManager.GetString("Description", resourceCulture);
            }
        }
        
        public static string SelectAPublisher {
            get {
                return ResourceManager.GetString("SelectAPublisher", resourceCulture);
            }
        }
        
        public static string ProjectArgumentDescription {
            get {
                return ResourceManager.GetString("ProjectArgumentDescription", resourceCulture);
            }
        }
        
        public static string OutputPathArgumentDescription {
            get {
                return ResourceManager.GetString("OutputPathArgumentDescription", resourceCulture);
            }
        }
        
        public static string GeneratingArtifacts {
            get {
                return ResourceManager.GetString("GeneratingArtifacts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PUBLISHING COMPLETED.
        /// </summary>
        public static string OperationCompletedPrefix {
            get {
                return ResourceManager.GetString("OperationCompletedPrefix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PUBLISHING FAILED.
        /// </summary>
        public static string OperationFailedPrefix {
            get {
                return ResourceManager.GetString("OperationFailedPrefix", resourceCulture);
            }
        }
    }
}
