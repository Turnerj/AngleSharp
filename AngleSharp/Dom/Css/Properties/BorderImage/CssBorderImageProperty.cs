﻿namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image
    /// </summary>
    sealed class CssBorderImageProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter ImageConverter = Converters.WithAny(
            Converters.OptionalImageSourceConverter.Option(),
            Converters.WithOrder(
                CssBorderImageSliceProperty.StyleConverter.Option(),
                CssBorderImageWidthProperty.StyleConverter.StartsWithDelimiter().Option(),
                CssBorderImageOutsetProperty.StyleConverter.StartsWithDelimiter().Option()),
            CssBorderImageRepeatProperty.StyleConverter.Option());

        #endregion

        #region ctor

        internal CssBorderImageProperty()
            : base(PropertyNames.BorderImage)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ImageConverter; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CssValue value)
        {
            return ImageConverter.Convert(value) != null;
            //TODO Convert instead of validate
            /*, m =>
            {
                Get<CssBorderImageSourceProperty>().TrySetValue(m.Item1);
                Get<CssBorderImageSliceProperty>().TrySetValue(m.Item2.Item1);
                Get<CssBorderImageWidthProperty>().TrySetValue(m.Item2.Item2);
                Get<CssBorderImageOutsetProperty>().TrySetValue(m.Item2.Item3);
                Get<CssBorderImageRepeatProperty>().TrySetValue(m.Item3);
            });*/
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var source = properties.OfType<CssBorderImageSourceProperty>().FirstOrDefault();
            var slice = properties.OfType<CssBorderImageSliceProperty>().FirstOrDefault();
            var width = properties.OfType<CssBorderImageWidthProperty>().FirstOrDefault();
            var outset = properties.OfType<CssBorderImageOutsetProperty>().FirstOrDefault();
            var repeat = properties.OfType<CssBorderImageRepeatProperty>().FirstOrDefault();

            if (source == null || slice == null || width == null || outset == null || repeat == null)
                return String.Empty;

            var values = new List<String>();
            values.Add(source.SerializeValue());

            if (slice.HasValue)
                values.Add(slice.SerializeValue());

            if (width.HasValue || outset.HasValue)
            {
                values.Add("/");

                if (width.HasValue)
                    values.Add(width.SerializeValue());

                if (outset.HasValue)
                {
                    values.Add("/");
                    values.Add(outset.SerializeValue());
                }
            }

            if (repeat.HasValue)
                values.Add(repeat.SerializeValue());

            return String.Format(" ", values);
        }

        #endregion
    }
}
