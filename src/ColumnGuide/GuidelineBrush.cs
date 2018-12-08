﻿using Microsoft.VisualStudio.Text.Classification;
using System;
using System.Windows.Media;

namespace ColumnGuide
{
    internal class GuidelineBrush
    {
        private Brush _brush;
        private readonly IEditorFormatMap _formatMap;

        public GuidelineBrush(IEditorFormatMap formatMap)
        {
            _formatMap = formatMap;
            _formatMap.FormatMappingChanged += OnFormatMappingChanged;
        }

        private void OnFormatMappingChanged(object sender, FormatItemsEventArgs e)
        {
            if (e.ChangedItems.Contains(GuidelineColorDefinition.Name))
            {
                _brush = GetGuidelineBrushFromFontsAndColors();
                BrushChanged?.Invoke(this, _brush);
            }
        }

        public Brush Brush => _brush ?? (_brush = GetGuidelineBrushFromFontsAndColors());

        public event EventHandler<Brush> BrushChanged;

        private Brush GetGuidelineBrushFromFontsAndColors()
        {
            var resourceDictionary = _formatMap.GetProperties(GuidelineColorDefinition.Name);
            if (resourceDictionary.Contains(EditorFormatDefinition.BackgroundBrushId))
            {
                return resourceDictionary[EditorFormatDefinition.BackgroundBrushId] as Brush;
            }

            return null;
        }
    }
}