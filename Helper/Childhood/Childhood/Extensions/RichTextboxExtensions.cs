using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Childhood.Extensions
{
    /// <summary>
    /// Расширение для RichTextBox
    /// </summary>
    public static class RichTextboxExtensions
    {
        /// <summary>
        /// Установить текст
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="text"></param>
        public static void SetText(this RichTextBox richTextBox, string text)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.Document.Blocks.Add(new Paragraph(new Run(text)));
        }

        /// <summary>
        /// Получить текст
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <returns></returns>
        public static string GetText(this RichTextBox richTextBox)
        {
            return new TextRange(richTextBox.Document.ContentStart,
                richTextBox.Document.ContentEnd).Text;
        }
    }
}
