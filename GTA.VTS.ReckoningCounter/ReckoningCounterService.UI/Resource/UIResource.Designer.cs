﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18063
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReckoningCounterService.UI.Resource {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class UIResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal UIResource() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ReckoningCounterService.UI.Resource.UIResource", typeof(UIResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似于 (Icon) 的 System.Drawing.Icon 类型的本地化资源。
        /// </summary>
        internal static System.Drawing.Icon CounterIcon {
            get {
                object obj = ResourceManager.GetObject("CounterIcon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   查找类似 关于(&amp;A) 的本地化字符串。
        /// </summary>
        internal static string MiAbout {
            get {
                return ResourceManager.GetString("MiAbout", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 实时行情(&amp;R) 的本地化字符串。
        /// </summary>
        internal static string MiRealtime {
            get {
                return ResourceManager.GetString("MiRealtime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 断开连接(&amp;B) 的本地化字符串。
        /// </summary>
        internal static string MiRealtimeBreakConnection {
            get {
                return ResourceManager.GetString("MiRealtimeBreakConnection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 重新连接(&amp;R) 的本地化字符串。
        /// </summary>
        internal static string MiRealtimeReconnect {
            get {
                return ResourceManager.GetString("MiRealtimeReconnect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 系统(&amp;S) 的本地化字符串。
        /// </summary>
        internal static string MiSystem {
            get {
                return ResourceManager.GetString("MiSystem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 退出(&amp;Q) 的本地化字符串。
        /// </summary>
        internal static string MiSystemQuit {
            get {
                return ResourceManager.GetString("MiSystemQuit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似于 (Icon) 的 System.Drawing.Icon 类型的本地化资源。
        /// </summary>
        internal static System.Drawing.Icon Reckon {
            get {
                object obj = ResourceManager.GetObject("Reckon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   查找 System.Drawing.Bitmap 类型的本地化资源。
        /// </summary>
        internal static System.Drawing.Bitmap Reckon1 {
            get {
                object obj = ResourceManager.GetObject("Reckon1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查找类似 行情服务连接状态: 的本地化字符串。
        /// </summary>
        internal static string SSStautBarLabel {
            get {
                return ResourceManager.GetString("SSStautBarLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似  的本地化字符串。
        /// </summary>
        internal static string SSStautBarText_A {
            get {
                return ResourceManager.GetString("SSStautBarText_A", resourceCulture);
            }
        }
    }
}
