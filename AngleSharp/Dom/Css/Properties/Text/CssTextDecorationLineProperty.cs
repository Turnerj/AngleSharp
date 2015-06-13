﻿namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-line
    /// Gets the enumeration over all selected styles for text decoration
    /// lines.
    /// </summary>
    sealed class CssTextDecorationLineProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter SingleConverter = Map.TextDecorationLines.ToConverter().Many();
        internal static readonly IValueConverter ListConverter = SingleConverter.OrNone();

        #endregion

        #region ctor

        internal CssTextDecorationLineProperty()
            : base(PropertyNames.TextDecorationLine)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Nothing
            get { return ListConverter; }
        }

        #endregion
    }
}
