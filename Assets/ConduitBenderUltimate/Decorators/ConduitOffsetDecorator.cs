using UnityEngine;
using System.Collections;
using System;

public class ConduitOffsetDecorator : AConduitDecorator
{
    public override void Decorate()
    {
  
    }

    public override void Highlight()
    {
        var bend = m_Conduit.bend;
        var highlight = bend.GetHighlight();
        var highlightColor = highlight.color;

        m_Conduit.SetHighlightColor( highlightColor );

        // Which parameter to highlight?
        if(highlight.name == EBendParameterName.DistanceBetween) {
            var start = m_Conduit.centerlineBendIndices[0];
            var end   = m_Conduit.centerlineBendIndices[2];
            
            ConduitGenerator.ColorConduit( m_Conduit, highlightColor, start.index, end.index );
        } else if(highlight.name == EBendParameterName.LengthOfBend) {
            var start = m_Conduit.centerlineBendIndices[2];
            var end   = m_Conduit.centerlineBendIndices[3];

            ConduitGenerator.ColorConduit( m_Conduit, highlightColor, start.index, end.index );
        }

    }

    public override void OnRemove()
    {
     
    }

    public override void Set( Conduit conduit )
    {
        m_Conduit = conduit;

    }
}
