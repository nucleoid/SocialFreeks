using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentMigrator.Builders.Execute;

namespace SocialFreeks.Database
{
    public static class MigrationExtensions
    {
        public static void ResourceScript(this IExecuteExpressionRoot root, Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new Exception(string.Format("Error retrieving resource {0} from assembly {1}", resourceName, assembly.FullName));
                }

                try
                {
                    using (var reader = new StreamReader(stream))
                    {
                        //The method IExecuteExpressionRoot.Sql calls a string format, therefor we have to escape 
                        //the { and } in some declarations. 
                        var script = reader.ReadToEnd().Replace("{", "{{").Replace("}", "}}");
                        var statements = SplitScriptByGo(script);

                        foreach (var statement in statements)
                        {
                            root.Sql(statement.Statement);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error using resource {0} from assembly {1}", resourceName, assembly.FullName), ex);
                }
            }
        }

        /// <summary>
        /// Sql statement
        /// </summary>
        private class SqlStatement
        {
            public string Statement = string.Empty;
            public int Line = 0;
        }

        /// <summary>
        /// Split script by GO
        /// </summary>
        /// <param name="script">sql script</param>
        /// <returns>statements array</returns>
        private static SqlStatement[] SplitScriptByGo(string script)
        {
            int curPos = 0;
            int line = 1;
            int length = script.Length;
            int lastLineEnd = 0;
            int curLineEnd = 0;
            int lastStatementEnd = 0;
            int lastCrSize = 0;
            var statements = new List<SqlStatement>();

            while (curPos < length)
            {
                if ((curPos + 1 < length) && script.Substring(curPos, 2) == "\r\n")
                {
                    curLineEnd = curPos;
                    curPos += 2;
                    lastCrSize = 2;
                    line++;
                }
                else if (script.Substring(curPos, 1) == "\n")
                {
                    curLineEnd = curPos;
                    curPos += 1;
                    lastCrSize = 1;
                    line++;
                }

                if (curLineEnd - lastLineEnd > 0)
                {
                    string curLine = script.Substring(lastLineEnd, curLineEnd - lastLineEnd + 1);

                    if (curLine.ToUpper().Trim() == "GO")
                    {
                        SqlStatement statement = new SqlStatement();

                        if (lastLineEnd >= lastStatementEnd)
                        {
                            statement.Statement = script.Substring(lastStatementEnd, lastLineEnd - lastStatementEnd + 1);
                        }

                        statement.Line = line;
                        statements.Add(statement);
                        lastStatementEnd = curLineEnd + lastCrSize;
                    }
                }

                lastLineEnd = curLineEnd;
                curPos++;
            }

            if ((length - 1) - lastStatementEnd > 0)
            {
                string curLine = script.Substring(lastLineEnd, (length - 1) - lastLineEnd + 1);

                if (curLine.ToUpper().Trim() == "GO")
                {
                    SqlStatement statement = new SqlStatement();

                    if (lastLineEnd >= lastStatementEnd)
                    {
                        statement.Statement = script.Substring(lastStatementEnd, lastLineEnd - lastStatementEnd + 1);
                    }

                    statement.Line = line;
                    statements.Add(statement);
                    lastStatementEnd = curLineEnd;
                }
                else
                {
                    if (((length - 1) - lastStatementEnd) > 0)
                    {
                        SqlStatement statement = new SqlStatement();
                        statement.Statement = script.Substring(lastStatementEnd, length - lastStatementEnd);
                        statement.Line = line;
                        statements.Add(statement);
                        lastStatementEnd = curLineEnd;
                    }
                }
            }

            if (statements.Count == 0)
            {
                SqlStatement statement = new SqlStatement();
                statement.Statement = script;
                statement.Line = line;
                statements.Add(statement);
            }

            return statements.Where(x => !String.IsNullOrEmpty(x.Statement.Trim())).ToArray();
        }
    }
}
