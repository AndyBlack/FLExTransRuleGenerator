// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.FLExTransRuleGen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.FLExTransRuleGen.Service
{
    public class WebPageProducer
    {
        private static readonly WebPageProducer instance = new WebPageProducer();

        RuleIdentifierSetter ruleIdSetter;

        private FLExTransRule rule;
        public static WebPageProducer Instance
        {
            get { return instance; }
        }

        public string ProduceWebPage(FLExTransRule rule)
        {
            this.rule = rule;
            ruleIdSetter = RuleIdentifierSetter.Instance;
            ruleIdSetter.SetIdentifiers(rule);
            StringBuilder sb = new StringBuilder();
            sb.Append(HtmlBeginning());
            sb.Append(HtmlBody());
            sb.Append(HtmlEnding());
            return sb.ToString();
        }

        private string HtmlBeginning()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                "<!DOCTYPE html SYSTEM \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n"
            );
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
            sb.Append("<head><title>");
            sb.Append(rule.Name);
            sb.Append("</title>\n");
            sb.Append("<meta charset=\"utf-8\"/>\n");
            sb.Append(
                "<link rel=\"stylesheet\" href=\"node_modules/treeflex/dist/css/treeflex.css\"/>\n"
            );
            sb.Append("<link rel=\"stylesheet\" href=\"rulegen.css\"/>\n");
            sb.Append("<script>\n");
            sb.Append(JavaScriptContents());
            sb.Append("</script>\n");
            sb.Append("</head>\n");
            sb.Append("<body>\n");
            return sb.ToString();
        }

        private string JavaScriptContents()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("function toApp(msg) {\n");
            sb.Append("window.chrome.webview.postMessage(msg);\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        private string HtmlBody()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>\n");
            sb.Append("<tr>\n");
            sb.Append(PhraseHTML(rule.Source.Phrase));
            sb.Append("<td>\n");
            sb.Append("<span class=\"arrow\"/>\n");
            sb.Append("</td>\n");
            sb.Append(PhraseHTML(rule.Target.Phrase));
            sb.Append("</tr>\n");
            sb.Append("</table>\n");
            return sb.ToString();
        }

        private string PhraseHTML(Phrase phrase)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<td valign=\"top\">\n");
            sb.Append("<div class=\"tf-tree tf-gap-sm\">\n");
            sb.Append("<ul>\n");
            sb.Append(phrase.ProduceHtml());
            sb.Append("</ul>\n");
            sb.Append("</div>\n");
            sb.Append("</td>\n");
            return sb.ToString();
        }

        private string HtmlEnding()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</body>\n");
            sb.Append("</html>\n");
            return sb.ToString();
        }
    }
}
