using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

/// <summary>
/// One of these should be present in the Scene for any Font recipe (Font & Font Style combination) which 
/// you want to generate size Metrics for.
/// </summary>
//[ExecuteInEditMode]
public class TextExtra : MonoBehaviour {


    private struct MaxChar
    {
        public float x;
        public float y;
        public char  characterX;
        public char  characterY;
    }

    public Font  font
    {
        get { return m_Font; }
        set {
            // Clear Dictionaries?
            if(value != m_Font) {
                Clear();
            }

            m_Font = value;
        }
    }
    public FontStyle fontStyle
    {
        get { return m_FontStyle; }
        set
        {
            // Clear Dictionaries?
            if(value != m_FontStyle) {
                Clear();
            }
            m_FontStyle = value;
        }
    }

    [Tooltip("A hidden Text element where measurements can be made.")]
    public Text  hiddenText;


    public int   minFontSize;
    public int   maxFontSize;
    public int   fontStepSize;

    //----------------------
    //    Private Data
    //----------------------
    private Dictionary<int, MaxChar>    m_FontSizeToMaxCharSize = new Dictionary<int, MaxChar>();

    [SerializeField, Tooltip( "Font type on which to calculate Metrics." )]
    private Font m_Font;
    [SerializeField]
    private FontStyle m_FontStyle;

    private bool m_HasInitialized = false;

    void Awake()
    {

        if(hiddenText == null) {
            Debug.LogError( "TextExtra: Awake() Hidden Text is null." );
            return;
        }
        if (m_Font == null) {
            Debug.LogError( "TextExtra: Awake() Font is null." );
            return;
        }

        Calculate( );

        m_HasInitialized = true;
        //Debug.Log( "TextExtra: Awake() ");
    }
    // Use this for initialization
    void Start () {
        //TextGenerator tg = new TextGenerator();
        //Debug.Log( "TextMagic: Start() Text value: " + text.text );
        //text.text = "<color=#ffaa00>Blarrggg</color>";

    }
	
    void OnValidate()
    {
        if (!m_HasInitialized) { return; }
        
    }

    //public void GetAllSupportedCharacters(Font font, out List<char> characters )
    //{
    //    characters = new List<char>();
    //    //System.Text.Encoding.UTF8.GetBytes( chars )
    //    //System.Text.Encoding.UTF16.GetString( byte[] )
    //    Encoding UTF16Enc = Encoding.GetEncoding( "utf-16" );
    //    char[] nextChar = new char[1];
    //    char[] nextUTF16Chars;
    //    for (char c = char.MinValue; c < char.MaxValue; ++c) {
    //        nextChar[ 0 ] = c;
    //        nextUTF16Chars = UTF16Enc.GetChars( UTF16Enc.GetBytes( nextChar ) );

    //        if (nextUTF16Chars.Length > 0) {
    //            char UTF8Char = nextUTF16Chars[0];
    //            if (font.HasCharacter( UTF8Char )) {
    //                characters.Add( UTF8Char );
    //            }
    //        }
    //    }
    //}

    /// <summary>
    /// Calculate Font Size Metrics for Accurate Fitting
    /// Should be called if changing min/max font sizes, font step size, or font style, or when using new characters
    /// </summary>
    private void Calculate( )
    {
        // Get all possible characters for selected Language from Engine/Localization
        string allChars = Regex.Replace( Engine.GetLanguageCharacters(), "(<.*?>)|(\\s+)", string.Empty );
        for (int fs = minFontSize; fs <= maxFontSize; fs += fontStepSize) {
            CalculateFontMetrics( fs, allChars );
        }
    }
    /// <summary>
    /// Calculates and sets Text font size to maximum Font Size at which specified # characters per line will fit. and text is contained in bounds height.
    /// Returns best size
    /// </summary>
    public int CalculateBestFontSize(Text text, Vector2 boundsSize, int charPerLine)
    {
        if(charPerLine < 1) {
            Debug.LogError( "TextExtra: CalculateBestFontSize() Characters per line must be a natural number except 0." );
            return -1;
        }
        if(text.font != font || text.fontStyle != fontStyle) {
            Debug.LogWarning( "TextExtra: CalculateBestFontSize() Given Text style does not match." );
        }      

        // Copy Text Component Values
        hiddenText.font = font;
        hiddenText.fontStyle = fontStyle;

        // Init Vars
        int     bestFit = minFontSize;
        float   width, height;
        MaxChar maxCharSize;
        // Loop through Font Sizes
        for (int fs = minFontSize; fs <= maxFontSize; fs += fontStepSize) 
        {
            if (m_FontSizeToMaxCharSize.TryGetValue( fs, out maxCharSize )) 
            {
                // @TODO - Binary Search?
                // Measure Hidden Text Component
                hiddenText.fontSize = fs;

                hiddenText.text = new string( maxCharSize.characterX, charPerLine );
                width = LayoutUtility.GetPreferredWidth( (RectTransform)hiddenText.transform );
                hiddenText.text = new string( maxCharSize.characterY, charPerLine );
                height = LayoutUtility.GetPreferredHeight( (RectTransform)hiddenText.transform );

                //Debug.Log( "Font Size: " + fs + " Preferred Width: " + width + " Bounds Width: " + bounds.rect.size.x );
                //if ((maxCharSize.x * charPerLine) / text.pixelsPerUnit < m_Bounds.rect.size.x) {
                if (width < boundsSize.x && height < boundsSize.y) {
                    bestFit = fs;
                } else {
                    break;
                }
            }
        }
        Debug.Log( "TextExtra: CalculateBestFontSize() BestFit: " + bestFit );

        return bestFit;
    }
    /// <summary>
    /// Clears the Font Metrics
    /// Should only be called when changing Font or Font style and before calling ReCalculate
    /// </summary>
    public void Clear()
    {
        m_FontSizeToMaxCharSize.Clear();

        Debug.Log( "<color=#ff0000>TextExtra: Clear()</color>" );
    }
    

    private void CalculateFontMetrics(int fontSize, string allChars )
    {
        // Look for Maximum Size
        MaxChar maxChar;
        maxChar.x = maxChar.y = 0f;
        maxChar.characterX = maxChar.characterY = allChars[ 0 ];

        // Ensure our Font Texture contains the current characters
        font.RequestCharactersInTexture( allChars, fontSize );

        //Loop through all characters
        CharacterInfo charInfo;
        Vector2 charSize;
        for (int i = 0; i < allChars.Length; i++) 
        {
            //Make sure character exists in font texture
            if (font.GetCharacterInfo( allChars[ i ], out charInfo, fontSize ) ) 
            {
                charSize.x = charInfo.maxX; //Mathf.Max( charInfo.maxX, charInfo.glyphWidth );
                charSize.y = charInfo.maxY; //Mathf.Max( charInfo.maxY, charInfo.glyphHeight );

                if (charSize.x > maxChar.x) {
                    maxChar.x = charSize.x;
                    maxChar.characterX = allChars[ i ];
                }
                if (charSize.y > maxChar.y) {
                    maxChar.y = charSize.y;
                    maxChar.characterY = allChars[ i ];
                }
            }
        }
        MaxChar currMaxSize;
        bool containsMaxSize = m_FontSizeToMaxCharSize.TryGetValue( fontSize, out currMaxSize );

        // Update Maximum Detected Character Size for this Specific Font Size
        if(containsMaxSize) {
            if (maxChar.x > currMaxSize.x) {
                currMaxSize.x = maxChar.x;
                maxChar.characterX = currMaxSize.characterX;
            }
            if(maxChar.y > currMaxSize.y) {
                currMaxSize.y = maxChar.y;
                maxChar.characterY = currMaxSize.characterY;
            }

            m_FontSizeToMaxCharSize.Remove( fontSize );
            m_FontSizeToMaxCharSize.Add( fontSize, currMaxSize );
        } else {
            m_FontSizeToMaxCharSize.Add( fontSize, maxChar );
        }


        //Debug.Log( "TextExtra: PrintCharMetrics() All Chars: " + allChars );
        //Debug.Log( "TextExtra: PrintCharMetrics() Font Size: " + fontSize + " Max Char Size: " + maxChar );
    }


}
