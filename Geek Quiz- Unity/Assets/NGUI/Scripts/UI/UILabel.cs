//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

#if !UNITY_3_5 && !UNITY_FLASH
#define DYNAMIC_FONT
#endif

using UnityEngine;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Label")]
public class UILabel : UIWidget
{
	public enum Effect
	{
		None,
		Shadow,
		Outline,
	}

	[HideInInspector][SerializeField] UIFont mFont;
	[HideInInspector][SerializeField] string mText = "";
	[HideInInspector][SerializeField] int mMaxLineWidth = 0;
	[HideInInspector][SerializeField] int mMaxLineHeight = 0;
	[HideInInspector][SerializeField] bool mEncoding = true;
	[HideInInspector][SerializeField] int mMaxLineCount = 0; // 0 denotes unlimited
	[HideInInspector][SerializeField] bool mPassword = false;
	[HideInInspector][SerializeField] bool mShowLastChar = false;
	[HideInInspector][SerializeField] Effect mEffectStyle = Effect.None;
	[HideInInspector][SerializeField] Color mEffectColor = Color.black;
	[HideInInspector][SerializeField] UIFont.SymbolStyle mSymbols = UIFont.SymbolStyle.Uncolored;
	[HideInInspector][SerializeField] Vector2 mEffectDistance = Vector2.one;
	[HideInInspector][SerializeField] bool mShrinkToFit = false;

	/// <summary>
	/// Obsolete, do not use. Use 'mMaxLineWidth' instead.
	/// </summary>

	[HideInInspector][SerializeField] float mLineWidth = 0;

	/// <summary>
	/// Obsolete, do not use. Use 'mMaxLineCount' instead
	/// </summary>

	[HideInInspector][SerializeField] bool mMultiline = true;

	bool mShouldBeProcessed = true;
	string mProcessedText = null;
	Vector3 mLastScale = Vector3.one;
	Vector2 mSize = Vector2.zero;
	bool mPremultiply = false;

	/// <summary>
	/// Function used to determine if something has changed (and thus the geometry must be rebuilt)
	/// </summary>

	bool hasChanged
	{
		get
		{
			return mShouldBeProcessed;
		}
		set
		{
			if (value)
			{
				mChanged = true;
				mShouldBeProcessed = true;
			}
			else
			{
				mShouldBeProcessed = false;
			}
		}
	}

	/// <summary>
	/// Retrieve the material used by the font.
	/// </summary>

	public override Material material { get { return (mFont != null) ? mFont.material : null; } }

	/// <summary>
	/// Set the font used by this label.
	/// </summary>

	public UIFont font
	{
		get
		{
			return mFont;
		}
		set
		{
			if (mFont != value)
			{
#if DYNAMIC_FONT
				if (mFont != null && mFont.dynamicFont != null)
					mFont.dynamicFont.textureRebuildCallback -= MarkAsChanged;
#endif
				RemoveFromPanel();
				mFont = value;
				hasChanged = true;
#if DYNAMIC_FONT
				if (mFont != null && mFont.dynamicFont != null)
				{
					mFont.dynamicFont.textureRebuildCallback += MarkAsChanged;
					mFont.Request(mText);
				}
#endif
				MarkAsChanged();
			}
		}
	}

	/// <summary>
	/// Text that's being displayed by the label.
	/// </summary>

	public string text
	{
		get
		{
			return mText;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(mText)) mText = "";
				hasChanged = true;
			}
                //mudamos, mesma rua n145.
			else //if (mText != value)
			{
				mText = value;
				hasChanged = true;
#if DYNAMIC_FONT
				if (mFont != null) mFont.Request(value);
#endif
				if (shrinkToFit) MakePixelPerfect();
			}
            if (hasChanged) ProcessText();//extra extra adição
		}
	}

	/// <summary>
	/// Whether this label will support color encoding in the format of [RRGGBB] and new line in the form of a "\\n" string.
	/// </summary>

	public bool supportEncoding
	{
		get
		{
			return mEncoding;
		}
		set
		{
			if (mEncoding != value)
			{
				mEncoding = value;
				hasChanged = true;
				if (value) mPassword = false;
			}
		}
	}

	/// <summary>
	/// Style used for symbols.
	/// </summary>

	public UIFont.SymbolStyle symbolStyle
	{
		get
		{
			return mSymbols;
		}
		set
		{
			if (mSymbols != value)
			{
				mSymbols = value;
				hasChanged = true;
			}
		}
	}

	/// <summary>
	/// Maximum width of the label in pixels.
	/// </summary>

	public int lineWidth
	{
		get
		{
			return mMaxLineWidth;
		}
		set
		{
			if (mMaxLineWidth != value)
			{
				mMaxLineWidth = value;
				hasChanged = true;
				if (shrinkToFit) MakePixelPerfect();
			}
		}
	}

	/// <summary>
	/// Maximum height of the label in pixels.
	/// </summary>

	public int lineHeight
	{
		get
		{
			return mMaxLineHeight;
		}
		set
		{
			if (mMaxLineHeight != value)
			{
				mMaxLineHeight = value;
				hasChanged = true;
				if (shrinkToFit) MakePixelPerfect();
			}
		}
	}

	/// <summary>
	/// Whether the label supports multiple lines.
	/// </summary>
	
	public bool multiLine
	{
		get
		{
			return mMaxLineCount != 1;
		}
		set
		{
			if ((mMaxLineCount != 1) != value)
			{
				mMaxLineCount = (value ? 0 : 1);
				hasChanged = true;
				if (value) mPassword = false;
			}
		}
	}

	/// <summary>
	/// The max number of lines to be displayed for the label
	/// </summary>

	public int maxLineCount
	{
		get
		{
			return mMaxLineCount;
		}
		set
		{
			if (mMaxLineCount != value)
			{
				mMaxLineCount = Mathf.Max(value, 0);
				if (value != 1) mPassword = false;
				hasChanged = true;
				if (shrinkToFit) MakePixelPerfect();
			}
		}
	}

	/// <summary>
	/// Whether the label's contents should be hidden
	/// </summary>

	public bool password
	{
		get
		{
			return mPassword;
		}
		set
		{
			if (mPassword != value)
			{
				if (value)
				{
					mMaxLineCount = 1;
					mEncoding = false;
				}
				mPassword = value;
				hasChanged = true;
			}
		}
	}

	/// <summary>
	/// Whether the last character of a password field will be shown
	/// </summary>

	public bool showLastPasswordChar
	{
		get
		{
			return mShowLastChar;
		}
		set
		{
			if (mShowLastChar != value)
			{
				mShowLastChar = value;
				hasChanged = true;
			}
		}
	}

	/// <summary>
	/// What effect is used by the label.
	/// </summary>

	public Effect effectStyle
	{
		get
		{
			return mEffectStyle;
		}
		set
		{
			if (mEffectStyle != value)
			{
				mEffectStyle = value;
				hasChanged = true;
			}
		}
	}

	/// <summary>
	/// Color used by the effect, if it's enabled.
	/// </summary>

	public Color effectColor
	{
		get
		{
			return mEffectColor;
		}
		set
		{
			if (!mEffectColor.Equals(value))
			{
				mEffectColor = value;
				if (mEffectStyle != Effect.None) hasChanged = true;
			}
		}
	}

	/// <summary>
	/// Effect distance in pixels.
	/// </summary>

	public Vector2 effectDistance
	{
		get
		{
			return mEffectDistance;
		}
		set
		{
			if (mEffectDistance != value)
			{
				mEffectDistance = value;
				hasChanged = true;
			}
		}
	}

	/// <summary>
	/// Whether the label will automatically shrink its size in order to fit the maximum line width.
	/// </summary>

	public bool shrinkToFit
	{
		get
		{
			return mShrinkToFit;
		}
		set
		{
			if (mShrinkToFit != value)
			{
				mShrinkToFit = value;
				hasChanged = true;
			}
		}
	}

	/// <summary>
	/// Returns the processed version of 'text', with new line characters, line wrapping, etc.
	/// </summary>

	public string processedText
	{
		get
		{
			if (mLastScale != cachedTransform.localScale)
			{
				mLastScale = cachedTransform.localScale;
				mShouldBeProcessed = true;
			}

			// Process the text if necessary
			if (hasChanged) ProcessText();
			return mProcessedText;
		}
	}

	/// <summary>
	/// Visible size of the widget in local coordinates.
	/// </summary>

	public override Vector2 relativeSize
	{
		get
		{
			if (mFont == null) return Vector3.one;
			if (hasChanged) ProcessText();
			return mSize;
		}
	}

#if DYNAMIC_FONT
	/// <summary>
	/// Register the font texture change listener.
	/// </summary>

	protected override void OnEnable ()
	{
		if (mFont != null && mFont.dynamicFont != null)
			mFont.dynamicFont.textureRebuildCallback += MarkAsChanged;
		base.OnEnable();
	}

	/// <summary>
	/// Remove the font texture change listener.
	/// </summary>

	protected override void OnDisable ()
	{
		if (mFont != null && mFont.dynamicFont != null)
			mFont.dynamicFont.textureRebuildCallback -= MarkAsChanged;
		base.OnDisable();
	}
#endif

	/// <summary>
	/// Determine start-up values.
	/// </summary>

	protected override void OnStart ()
	{
		// Legacy support
		if (mLineWidth > 0f)
		{
			mMaxLineWidth = Mathf.RoundToInt(mLineWidth);
			mLineWidth = 0f;
		}

		if (!mMultiline)
		{
			mMaxLineCount = 1;
			mMultiline = true;
		}

		// Whether this is a premultiplied alpha shader
		mPremultiply = (font != null && font.material != null && font.material.shader.name.Contains("Premultiplied"));

#if DYNAMIC_FONT
		// Request the text within the font
		if (mFont != null) mFont.Request(mText);
#endif
	}

#if UNITY_EDITOR
	/// <summary>
	/// Labels are not resizable using the handles.
	/// </summary>

	public override bool showResizeHandles { get { return false; } }
#endif

	/// <summary>
	/// UILabel needs additional processing when something changes.
	/// </summary>

	public override void MarkAsChanged ()
	{
		hasChanged = true;
		base.MarkAsChanged();
	}

	/// <summary>
	/// Process the raw text, called when something changes.
	/// </summary>

	public void ProcessText ()
	{
		mChanged = true;
		hasChanged = false;

		float scale = Mathf.Abs(cachedTransform.localScale.x);

		if (scale > 0f)
		{
			for (;;)
			{
				bool fits = true;

				if (mPassword)
				{
					mProcessedText = "";

					if (mShowLastChar)
					{
						for (int i = 0, imax = mText.Length - 1; i < imax; ++i)
							mProcessedText += "*";
						if (mText.Length > 0)
							mProcessedText += mText[mText.Length - 1];
					}
					else
					{
						for (int i = 0, imax = mText.Length; i < imax; ++i)
							mProcessedText += "*";
					}
					fits = mFont.WrapText(mProcessedText, out mProcessedText, mMaxLineWidth / scale, mMaxLineHeight / scale,
						mMaxLineCount, false, UIFont.SymbolStyle.None);
				}
				else if (mMaxLineWidth > 0 || mMaxLineHeight > 0)
				{
					fits = mFont.WrapText(mText, out mProcessedText, mMaxLineWidth / scale, mMaxLineHeight / scale,
						mMaxLineCount, mEncoding, mSymbols);
				}
				else mProcessedText = mText;

				mSize = !string.IsNullOrEmpty(mProcessedText) ? mFont.CalculatePrintedSize(mProcessedText, mEncoding, mSymbols) : Vector2.one;

				if (mShrinkToFit)
				{
					// We want to shrink the label (when it doesn't fit)
					if (!fits)
					{
						scale = Mathf.Round(scale - 1f);
						if (scale > 1f) continue;
					}

					if (mMaxLineWidth > 0)
					{
						float maxX = (float)mMaxLineWidth / scale;
						float x = (mSize.x * scale > maxX) ? (maxX / mSize.x) * scale : scale;
						scale = Mathf.Min(x, scale);
					}

					scale = Mathf.Round(scale);
					cachedTransform.localScale = new Vector3(scale, scale, 1f);
				}
				break;
			}
			mSize.x = Mathf.Max(mSize.x, (scale > 0f) ? mMaxLineWidth / scale : 1f);
			mSize.y = Mathf.Max(mSize.y, (scale > 0f) ? mMaxLineHeight / scale : 1f);
		}
		else
		{
			// This should never happen (label should never have a scale of 0) -- but just in case.
			mSize.x = 1f;
			mSize.y = 1f;
			scale = mFont.size;

			cachedTransform.localScale = new Vector3(scale, scale, 1f);
			mProcessedText = "";
		}
		mSize.y = Mathf.Max(Mathf.Max(mSize.y, 1f), mMaxLineHeight / scale);
	}

	/// <summary>
	/// Text is pixel-perfect when its scale matches the size.
	/// </summary>

	public override void MakePixelPerfect ()
	{
		if (mFont != null)
		{
			float pixelSize = font.pixelSize;

			Vector3 scale = cachedTransform.localScale;
			scale.x = mFont.size * pixelSize;
			scale.y = scale.x;
			scale.z = 1f;

			Vector3 pos = cachedTransform.localPosition;
			pos.x = (Mathf.CeilToInt(pos.x / pixelSize * 4f) >> 2);
			pos.y = (Mathf.CeilToInt(pos.y / pixelSize * 4f) >> 2);
			pos.z = Mathf.RoundToInt(pos.z);

			pos.x *= pixelSize;
			pos.y *= pixelSize;

			cachedTransform.localPosition = pos;
			cachedTransform.localScale = scale;
			
			if (shrinkToFit) ProcessText();
		}
		else base.MakePixelPerfect();
	}

	/// <summary>
	/// Apply a shadow effect to the buffer.
	/// </summary>

	void ApplyShadow (BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, int start, int end, float x, float y)
	{
		Color c = mEffectColor;
		c.a *= alpha * mPanel.alpha;
		Color32 col = (font.premultipliedAlpha) ? NGUITools.ApplyPMA(c) : c;

		for (int i = start; i < end; ++i)
		{
			verts.Add(verts.buffer[i]);
			uvs.Add(uvs.buffer[i]);
			cols.Add(cols.buffer[i]);

			Vector3 v = verts.buffer[i];
			v.x += x;
			v.y += y;
			verts.buffer[i] = v;
			cols.buffer[i] = col;
		}
	}

	/// <summary>
	/// Draw the label.
	/// </summary>

	public override void OnFill (BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (mFont == null) return;
		Pivot p = pivot;
		int offset = verts.size;

		Color col = color;
		col.a *= mPanel.alpha;
		if (font.premultipliedAlpha) col = NGUITools.ApplyPMA(col);

		// Print the text into the buffers
		if (p == Pivot.Left || p == Pivot.TopLeft || p == Pivot.BottomLeft)
		{
			mFont.Print(processedText, col, verts, uvs, cols, mEncoding, mSymbols, UIFont.Alignment.Left, 0, mPremultiply);
		}
		else if (p == Pivot.Right || p == Pivot.TopRight || p == Pivot.BottomRight)
		{
			mFont.Print(processedText, col, verts, uvs, cols, mEncoding, mSymbols, UIFont.Alignment.Right,
				Mathf.RoundToInt(relativeSize.x * mFont.size), mPremultiply);
		}
		else
		{
			mFont.Print(processedText, col, verts, uvs, cols, mEncoding, mSymbols, UIFont.Alignment.Center,
				Mathf.RoundToInt(relativeSize.x * mFont.size), mPremultiply);
		}

		// Apply an effect if one was requested
		if (effectStyle != Effect.None)
		{
			int end = verts.size;
			float pixel = 1f / (mFont.size * mFont.pixelSize);

			float fx = pixel * mEffectDistance.x;
			float fy = pixel * mEffectDistance.y;

			ApplyShadow(verts, uvs, cols, offset, end, fx, -fy);

			if (effectStyle == Effect.Outline)
			{
				offset = end;
				end = verts.size;

				ApplyShadow(verts, uvs, cols, offset, end, -fx, fy);

				offset = end;
				end = verts.size;

				ApplyShadow(verts, uvs, cols, offset, end, fx, fy);

				offset = end;
				end = verts.size;

				ApplyShadow(verts, uvs, cols, offset, end, -fx, -fy);
			}
		}
	}
}
