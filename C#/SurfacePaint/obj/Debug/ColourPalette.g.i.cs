﻿#pragma checksum "..\..\ColourPalette.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C28D4BEA6B7FF49A5BA12E6064B73C09"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.1
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Controls.Primitives;
using Microsoft.Surface.Presentation.Controls.TouchVisualizations;
using Microsoft.Surface.Presentation.Input;
using Microsoft.Surface.Presentation.Palettes;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SurfacePaint {
    
    
    /// <summary>
    /// ColourPalette
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class ColourPalette : Microsoft.Surface.Presentation.Controls.TagVisualization, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\ColourPalette.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\ColourPalette.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse ellipse_color;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\ColourPalette.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton black;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SurfacePaint;component/colourpalette.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ColourPalette.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 5 "..\..\ColourPalette.xaml"
            ((SurfacePaint.ColourPalette)(target)).Loaded += new System.Windows.RoutedEventHandler(this.ColourPalette_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.ellipse_color = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 4:
            
            #line 8 "..\..\ColourPalette.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 9 "..\..\ColourPalette.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 10 "..\..\ColourPalette.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 11 "..\..\ColourPalette.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 12 "..\..\ColourPalette.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 13 "..\..\ColourPalette.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 14 "..\..\ColourPalette.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 15 "..\..\ColourPalette.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            case 12:
            this.black = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            
            #line 16 "..\..\ColourPalette.xaml"
            this.black.Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 17 "..\..\ColourPalette.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 18 "..\..\ColourPalette.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 19 "..\..\ColourPalette.xaml"
            ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target)).Click += new System.Windows.RoutedEventHandler(this.onclick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

