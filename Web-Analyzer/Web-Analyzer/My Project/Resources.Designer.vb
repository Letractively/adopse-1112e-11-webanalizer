﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.5456
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Web_Analyzer.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        Friend ReadOnly Property favicon_32x32() As System.Drawing.Icon
            Get
                Dim obj As Object = ResourceManager.GetObject("favicon_32x32", resourceCulture)
                Return CType(obj,System.Drawing.Icon)
            End Get
        End Property
        
        Friend ReadOnly Property images__4_() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("images__4_", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property images10() As System.Drawing.Icon
            Get
                Dim obj As Object = ResourceManager.GetObject("images10", resourceCulture)
                Return CType(obj,System.Drawing.Icon)
            End Get
        End Property
        
        Friend ReadOnly Property images8() As System.Drawing.Icon
            Get
                Dim obj As Object = ResourceManager.GetObject("images8", resourceCulture)
                Return CType(obj,System.Drawing.Icon)
            End Get
        End Property
        
        Friend ReadOnly Property statistics2() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("statistics2", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to image.
        '''</summary>
        Friend ReadOnly Property String1() As String
            Get
                Return ResourceManager.GetString("String1", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
