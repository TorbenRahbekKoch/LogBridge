using System.Text;

namespace SoftwarePassion.LogBridge
{
    internal class DescriptorBuilder
    {
        public void Indent()
        {
            indentLevel += 2;
        }

        public void Outdent()
        {
            indentLevel -= 2;
            if (indentLevel < 0)
                indentLevel = 0;
        }

        public void Append(string text)
        {
            if (justAddedNewLine)
            {
                justAddedNewLine = false;
                builder.Append(Spaces(indentLevel));
            }

            builder.Append(text);
        }

        public void AppendLine(string text)
        {
            if (justAddedNewLine)
            {
                justAddedNewLine = false;
                builder.Append(Spaces(indentLevel));
            }
            builder.AppendLine(text);
            justAddedNewLine = true;
        }

        public override string ToString()
        {
            return builder.ToString();
        }

        private static string Spaces(int count)
        {
            string s = string.Empty;
            while (s.Length < count)
            {
                s += "  ";
            }

            return s;
        }

        private bool justAddedNewLine = false;
        private short indentLevel = 0;
        private readonly StringBuilder builder = new StringBuilder();
    }
}