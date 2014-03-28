﻿namespace Dev.Demo.Entities.NewsAgg
{
    public class ItemContentFactory
    {
        public static ItemContent CreateItemContent(string title, string shortDes, string content)
        {
            return CreateItemContent(title, shortDes, content, null, null, null);
        }

        public static ItemContent CreateItemContent(string title, string shortDes, string content, string smallImagePath, string mediumImagePath, string largeImagePath)
        {
            return new ItemContent
                {
                    CreatedDate = System.DateTime.Now,
                    CreatedBy = "zbw",
                    Title = title,
                    SortDescription = shortDes,
                    Content = content,
                    SmallImage = smallImagePath,
                    MediumImage = mediumImagePath,
                    BigImage = largeImagePath
                };
        }
    }
}