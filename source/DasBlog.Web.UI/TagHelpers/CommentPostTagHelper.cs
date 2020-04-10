﻿using DasBlog.Core.Common;
using DasBlog.Services;
using DasBlog.Web.Models.BlogViewModels;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;


namespace DasBlog.Web.TagHelpers
{
	[Obsolete]
	public class CommentPostTagHelper : TagHelper
	{
		public PostViewModel Post { get; set; }
		public string LinkText { get; set; }

		private readonly IDasBlogSettings dasBlogSettings;

		public CommentPostTagHelper(IDasBlogSettings dasBlogSettings)
		{
			this.dasBlogSettings = dasBlogSettings;
		}

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "a";
			output.TagMode = TagMode.StartTagAndEndTag;
			output.Attributes.SetAttribute("href", dasBlogSettings.GetCommentViewUrl(Post.PermaLink));
			output.Attributes.SetAttribute("id", Constants.CommentOnThisPostId);
			output.Attributes.SetAttribute("class", "dbc-comment-on-post-link");

			string content;
			var commentCount = Post.Comments?.Comments.Count ?? 0;
			if (string.IsNullOrWhiteSpace(LinkText)) {
				content = $"Comment on this post [{commentCount}]";
			}
			else
			{
				content = string.Format(LinkText, commentCount);
			}

			output.Content.SetHtmlContent(content);
		}

		public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			return Task.Run(() => Process(context, output));
		}
	}
}
