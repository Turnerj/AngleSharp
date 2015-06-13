﻿namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration
    /// </summary>
    sealed class CssTextDecorationProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.ColorConverter.Option(),
            Converters.TextDecorationStyleConverter.Option(),
            CssTextDecorationLineProperty.ListConverter.Option());

        #endregion

        #region ctor

        internal CssTextDecorationProperty()
            : base(PropertyNames.TextDecoration, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CssValue value)
        {
            return StyleConverter.Convert(value) != null;
            //TODO Convert instead of validate
            /*, m =>
            {
                Get<CssTextDecorationColorProperty>().TrySetValue(m.Item1);
                Get<CssTextDecorationStyleProperty>().TrySetValue(m.Item2);
                Get<CssTextDecorationLineProperty>().TrySetValue(m.Item3);
            });*/
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var color = properties.OfType<CssTextDecorationColorProperty>().FirstOrDefault();
            var style = properties.OfType<CssTextDecorationStyleProperty>().FirstOrDefault();
            var line = properties.OfType<CssTextDecorationLineProperty>().FirstOrDefault();

            if (color == null || style == null || line == null)
                return String.Empty;

            var result = new List<String>();

            if (line.HasValue)
                result.Add(line.SerializeValue());

            if (style.HasValue)
                result.Add(style.SerializeValue());

            if (color.HasValue)
                result.Add(color.SerializeValue());

            return String.Join(" ", result);
        }

        #endregion
    }
}
