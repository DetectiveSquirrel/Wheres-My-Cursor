﻿using ExileCore;
using ExileCore.Shared.Helpers;
using SharpDX;
using Wheres_My_Cursor.libs;

namespace Wheres_My_Cursor;

public class Main : BaseSettingsPlugin<Settings>
{
    private Vector2 _clickWindowOffset;

    public override bool Initialise() => true;

    public override void Render()
    {
        WheresMyCursor();
    }

    private void WheresMyCursor()
    {
        if (!Settings.Enable)
        {
            return;
        }

        var windows = GameController.Game.IngameState.IngameUi;

        if (windows.TreePanel.IsVisible)
        {
            return;
        }

        if (windows.Atlas.IsVisible)
        {
            return;
        }

        if (windows.SyndicatePanel.IsVisible)
        {
            return;
        }

        if (windows.AtlasTreePanel.IsVisible)
        {
            return;
        }

        if (windows.OpenLeftPanel.IsVisible)
        {
            return;
        }

        if (windows.OpenRightPanel.IsVisible)
        {
            return;
        }

        var cursorPositionVector = Mouse.GetCursorPositionVector().ToVector2Num();
        var windowRectangle = GameController.Window.GetWindowRectangleReal();
        _clickWindowOffset = GameController.Window.GetWindowRectangle().TopLeft;
        cursorPositionVector -= _clickWindowOffset.ToVector2Num();

        // need to call use -170 in Z axis for player.pos.translate as this is whats used in poecore for hp bar.
        //i have no fucking clue why this is. If you do not follow this rule you will have a jumpy hp bar
        //var playerToScreen = ExileCore.PoEMemory.RemoteMemoryObject.pTheGame.IngameState.Camera.WorldToScreen(GameController.Player.PosNum.Translate(0, 0, -170));
        System.Numerics.Vector2 finalPointA;

        switch (Settings.WmcLineType)
        {
            case 0:
                finalPointA = Vector2OffsetCalculations(
                    new System.Numerics.Vector2(windowRectangle.Width / 2, windowRectangle.Height / 2)
                );

                DrawLineToPosWithLength(
                    finalPointA,
                    cursorPositionVector,
                    Settings.WmcLineLength,
                    Settings.WmcLineSize,
                    Settings.WmcLineColor
                );

                break;
            case 1:
                finalPointA = Vector2OffsetCalculations(
                    new System.Numerics.Vector2(windowRectangle.Width / 2, windowRectangle.Height / 2)
                );

                DrawLineToPos(finalPointA, cursorPositionVector, Settings.WmcLineSize, Settings.WmcLineColor);
                break;
            case 2:
                finalPointA = Vector2OffsetCalculations(
                    new System.Numerics.Vector2(windowRectangle.Width / 2, windowRectangle.Height / 2)
                );

                DrawLineToPosWithLength(
                    finalPointA,
                    cursorPositionVector,
                    (int)windowRectangle.Width,
                    Settings.WmcLineSize,
                    Settings.WmcLineColor
                );

                break;
        }
    }

    public System.Numerics.Vector2 Vector2OffsetCalculations(System.Numerics.Vector2 information)
    {
        var finalVectorCalculation = information;

        finalVectorCalculation.X = Settings.WmcPlayerOffsetXNegitive
            ? finalVectorCalculation.X - Settings.WmcPlayerOffsetX
            : finalVectorCalculation.X + Settings.WmcPlayerOffsetX;

        finalVectorCalculation.Y = Settings.WmcPlayerOffsetYNegitive
            ? finalVectorCalculation.Y - Settings.WmcPlayerOffsetY
            : finalVectorCalculation.Y + Settings.WmcPlayerOffsetY;

        return finalVectorCalculation;
    }

    public void DrawLineToPosWithLength(System.Numerics.Vector2 pointA, System.Numerics.Vector2 pointB, int lineLength,
        int lineSize, Color lineColor)
    {
        var direction = pointB - pointA;
        direction.ToSharpDx().Normalize();
        var pointC = pointA + direction * lineLength;
        Graphics.DrawLine(pointA, pointC, lineSize, lineColor);
    }

    public void DrawLineToPos(System.Numerics.Vector2 pointA, System.Numerics.Vector2 pointB, int lineSize,
        Color lineColor)
    {
        Graphics.DrawLine(pointA, pointB, lineSize, lineColor);
    }
}