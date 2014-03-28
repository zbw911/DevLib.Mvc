﻿namespace Dev.Demo.Mapping
{
    using Dev.Demo.Entities.NewsAgg;

    public class ItemContentMapping : EntityMappingBase<ItemContent>
    {
        public ItemContentMapping()
        {
            this.Property(x => x.Title).IsRequired();
            this.Property(x => x.SortDescription).IsRequired();
            this.Property(x => x.Content).IsRequired();
            this.Property(x => x.SmallImage).IsRequired();
            this.Property(x => x.MediumImage).IsRequired();
            this.Property(x => x.BigImage).IsRequired();
            this.Property(x => x.NumOfView).IsRequired();

            this.HasMany(x => x.Items).WithRequired(y => y.ItemContent);

            this.ToTable("ItemContent");
        }
    }
}