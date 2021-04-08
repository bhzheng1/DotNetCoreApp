/*
    本例构建了htmlhelper的三个扩展方法 
    一般把该类放在Microsoft.AspNetCore.Mvc.Rendering名称空间里，因为Razor engine 默认在这个名称空间里加载扩展方法。
    也可以放入其它名称空间，但必须在view的头部使用@use导入。
    扩展方法一般为static类

    HtmlHelper本质是利用C#代码构建html语句
*/

//@Html.ColorfulHeading(1, "green", "Welcome to Seattle")
//<h1 style="color:green">Welcome to Seattle</h1>
using System;
using System.Linq.Expressions;
using HtmlTags;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelperClassLibrary
{
    //扩展IHtmlHelper的方法
    public static class HtmlHelperExtensions
    {

        //利用扩展方法构建Html语句：<h1 style="color:green">Welcome to Seattle</h1> <h2 style="color:orange">Especially in Summer</h2>
        public static IHtmlContent ColorfulHeading(this IHtmlHelper htmlHelper, int level, string color, string content)
        {
            level = level < 1 ? 1 : level;
            level = level > 6 ? 6 : level;
            var tagName = $"h{level}";
            var tagBuilder = new TagBuilder(tagName);
            tagBuilder.Attributes.Add("style", $"color:{color ?? "green"}");
            tagBuilder.InnerHtml.Append(content ?? string.Empty);
            return tagBuilder;
        }

        //一般html语句中不能检查名称错误，而model binding可以自动检查名称拼写错误。
        //利用扩展方法构建Model Binding
        public static string GetName<TModel, TResult>(this IHtmlHelper<TModel> target, Expression<Func<TModel, TResult>> expression)
        {
            var body = expression.Body as MemberExpression;
            return body.Member.Name;
        }

        //constrain name and id attributes of html element
        //<input id="id" name="id" type="text"></input><br />
        //<input id="name" name="name" type="text"></input><br />
        //<input id="age" name="age" type="text"></input><br />

        public static IHtmlContent FakeTextBoxFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            var body = expression.Body as MemberExpression;
            var properName = body.Member.Name.ToLower();
            var tagBuilder = new TagBuilder("input");
            tagBuilder.Attributes.Add("type", "text");
            tagBuilder.Attributes.Add("id", properName);
            tagBuilder.Attributes.Add("name", properName);
            return tagBuilder;
        }

        public static IHtmlContent FakeTable(this IHtmlHelper htmlHelper, string[] columns, string[,] content)
        {
            var tableBuilder = new TagBuilder("table");
            tableBuilder.Attributes.Add("border", "1");

            var headerBuilder = new TagBuilder("tr");
            foreach (var column in columns)
            {
                var headerCellBuilder = new TagBuilder("th");
                headerCellBuilder.InnerHtml.Append(column);
                headerBuilder.InnerHtml.AppendHtml(headerCellBuilder);
            }

            tableBuilder.InnerHtml.AppendHtml(headerBuilder);

            int col = columns.Length;
            int row = content.Length / col;
            for (int i = 0; i < row; i++)
            {
                var rowBuilder = new TagBuilder("tr");
                for (int j = 0; j < col; j++)
                {
                    var cellBuilder = new TagBuilder("td");
                    cellBuilder.InnerHtml.Append(content[i, j]);
                    rowBuilder.InnerHtml.AppendHtml(cellBuilder);
                }

                tableBuilder.InnerHtml.AppendHtml(rowBuilder);
            }

            return tableBuilder;
        }

        public static HtmlTag ValidationDiv(this IHtmlHelper helper)
        {
            return new HtmlTag("div")
                .Id("validationSummary")
                .AddClass("alert")
                .AddClass("alert-danger")
                .AddClass("hidden");
        }

        public static HtmlTag FormBlock<T>(this IHtmlHelper<T> helper,
            Expression<Func<T, object>> expression,
            Action<HtmlTag> labelModifier = null,
            Action<HtmlTag> inputModifier = null
        ) where T : class
        {
            labelModifier = labelModifier ?? (_ => { });
            inputModifier = inputModifier ?? (_ => { });

            var divTag = new HtmlTag("div");
            divTag.AddClass("form-group");

            var labelTag = helper.Label(expression);
            labelModifier(labelTag);

            var inputTag = helper.Input(expression);
            inputModifier(inputTag);

            divTag.Append(labelTag);
            divTag.Append(inputTag);

            return divTag;
        }
    }
}