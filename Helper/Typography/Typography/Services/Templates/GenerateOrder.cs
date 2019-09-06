﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Typography.Services.Templates
{
    using System.Linq;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class GenerateOrder : GenerateOrderBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"<?xml version=""1.0"" encoding=""utf-8""?>
<Report xmlns:rd=""http://schemas.microsoft.com/SQLServer/reporting/reportdesigner"" xmlns=""http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition"">
  <DataSources>
    <DataSource Name=""DataSource1"">
      <ConnectionProperties>
        <DataProvider>System.Data.DataSet</DataProvider>
        <ConnectString>/* Local Connection */</ConnectString>
      </ConnectionProperties>
      <rd:DataSourceID>e9784bb0-a630-49cc-b7f9-8495aca23a6c</rd:DataSourceID>
    </DataSource>
  </DataSources>
  <DataSets>
    <DataSet Name=""DataSet1"">
      <Fields>
");
            
            #line 19 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
    foreach(ReportColumn column in Model){
            
            #line default
            #line hidden
            this.Write("        <Field Name=\"");
            
            #line 20 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(column.Name));
            
            #line default
            #line hidden
            this.Write("\">\r\n          <DataField>");
            
            #line 21 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(column.Name));
            
            #line default
            #line hidden
            this.Write("</DataField>\r\n          <rd:TypeName>");
            
            #line 22 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(column.Type.Name));
            
            #line default
            #line hidden
            this.Write("</rd:TypeName>\r\n        </Field>\r\n");
            
            #line 24 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
    }
            
            #line default
            #line hidden
            this.Write(@"      </Fields>
      <Query>
        <DataSourceName>DataSource1</DataSourceName>
        <CommandText>/* Local Query */</CommandText>
      </Query>
      <rd:DataSetInfo>
        <rd:DataSetName />
        <rd:TableName />
        <rd:ObjectDataSourceType />
      </rd:DataSetInfo>
    </DataSet>
  </DataSets>
  <Body>
    <ReportItems>
      <Tablix Name=""Tablix1"">
        <TablixBody>
          <TablixColumns>
");
            
            #line 42 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
    foreach(ReportColumn column in Model){
            
            #line default
            #line hidden
            this.Write("            <TablixColumn>\r\n              <Width>");
            
            #line 44 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(column.WidthInInch));
            
            #line default
            #line hidden
            this.Write("in</Width>\r\n            </TablixColumn>\r\n");
            
            #line 46 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
    }
            
            #line default
            #line hidden
            this.Write("          </TablixColumns>\r\n          <TablixRows>\r\n            <TablixRow>\r\n    " +
                    "          <Height>0.25in</Height>\r\n              <TablixCells>\r\n");
            
            #line 52 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
    foreach(ReportColumn column in Model){
            
            #line default
            #line hidden
            this.Write("                <TablixCell>\r\n                  <CellContents>\r\n                 " +
                    "   <Textbox Name=\"");
            
            #line 55 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(column.Name));
            
            #line default
            #line hidden
            this.Write(@"TextBox"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>");
            
            #line 62 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(column.Title));
            
            #line default
            #line hidden
            this.Write(@"</Value>
                              <Style />
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>");
            
            #line 69 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(column.Name));
            
            #line default
            #line hidden
            this.Write(@"TextBox</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>Solid</Style>
                        </Border>
                        <BackgroundColor>");
            
            #line 75 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(column.HeaderBackColorInHtml));
            
            #line default
            #line hidden
            this.Write(@"</BackgroundColor>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
");
            
            #line 84 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
    }
            
            #line default
            #line hidden
            this.Write("              </TablixCells>\r\n            </TablixRow>\r\n            <TablixRow>\r\n" +
                    "              <Height>0.25in</Height>\r\n              <TablixCells>\r\n");
            
            #line 90 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
    foreach(ReportColumn column in Model){
            
            #line default
            #line hidden
            this.Write("                <TablixCell>\r\n                  <CellContents>\r\n                 " +
                    "   <Textbox Name=\"");
            
            #line 93 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(column.Name));
            
            #line default
            #line hidden
            this.Write(@""">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>");
            
            #line 100 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(column.Expression));
            
            #line default
            #line hidden
            this.Write(@"</Value>
                              <Style />
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>");
            
            #line 107 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(column.Name));
            
            #line default
            #line hidden
            this.Write(@"</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>Solid</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
");
            
            #line 121 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
    }
            
            #line default
            #line hidden
            this.Write("              </TablixCells>\r\n            </TablixRow>\r\n          </TablixRows>\r\n" +
                    "        </TablixBody>\r\n        <TablixColumnHierarchy>\r\n          <TablixMembers" +
                    ">\r\n");
            
            #line 128 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
    foreach(ReportColumn column in Model){
            
            #line default
            #line hidden
            this.Write("            <TablixMember />\r\n");
            
            #line 130 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"
    }
            
            #line default
            #line hidden
            this.Write(@"          </TablixMembers>
        </TablixColumnHierarchy>
        <TablixRowHierarchy>
          <TablixMembers>
            <TablixMember>
              <KeepWithGroup>After</KeepWithGroup>
            </TablixMember>
            <TablixMember>
              <Group Name=""Details"" />
            </TablixMember>
          </TablixMembers>
        </TablixRowHierarchy>
        <DataSetName>DataSet1</DataSetName>
        <Top>0.15625in</Top>
        <Left>0.125in</Left>
        <Height>0.5in</Height>
        <Width>2in</Width>
        <Style>
          <Border>
            <Style>None</Style>
          </Border>
        </Style>
      </Tablix>
    </ReportItems>
    <Height>0.82292in</Height>
    <Style />
  </Body>
  <Width>6.5in</Width>
  <Page>
    <LeftMargin>1in</LeftMargin>
    <RightMargin>1in</RightMargin>
    <TopMargin>1in</TopMargin>
    <BottomMargin>1in</BottomMargin>
    <Style />
  </Page>
  <rd:ReportID>60987c40-62b1-463b-b670-f3fa81914e33</rd:ReportID>
  <rd:ReportUnitType>Inch</rd:ReportUnitType>
</Report>");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "D:\repositories\University\Helper\Typography\Typography\Services\Templates\GenerateOrder.tt"

private global::System.Collections.Generic.List<ReportColumn> _ModelField;

/// <summary>
/// Access the Model parameter of the template.
/// </summary>
private global::System.Collections.Generic.List<ReportColumn> Model
{
    get
    {
        return this._ModelField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool ModelValueAcquired = false;
if (this.Session.ContainsKey("Model"))
{
    this._ModelField = ((global::System.Collections.Generic.List<ReportColumn>)(this.Session["Model"]));
    ModelValueAcquired = true;
}
if ((ModelValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Model");
    if ((data != null))
    {
        this._ModelField = ((global::System.Collections.Generic.List<ReportColumn>)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public class GenerateOrderBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
