package com.example.paul.telecommande;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Matrix;
import android.graphics.Paint;
import android.graphics.Path;
import android.graphics.Point;
import android.graphics.Rect;
import android.util.Log;
import android.view.View;

/**
 * Created by Paul on 30/01/2015.
 */
public class Petals extends View {
    private Paint mTextPaint = new Paint();
    private int number;
    float radiusCircle = 80;
    float radius = 0;
    Bitmap imageButton;
    private float tailleFont = 24f;
    private int alpha = 80;
    private int speedGrowth = 4;

    public Petals(Context context, Bitmap img, int n) {
        super(context);
        number = n;
        imageButton = img;

    }

    @Override
    public void onDraw(Canvas canvas)
    {

        float centerX = this.getWidth()/2 - imageButton.getWidth()/2 + 18;
        float centerY = this.getHeight()/2 - imageButton.getHeight()/2 + 20;

        drawPetals(canvas, centerX, centerY);
        drawText(canvas, centerX, centerY);
    }

    public void drawPetals(Canvas canvas, float centerX, float centerY){
        //Draw Circle
        mTextPaint.setColor(Color.RED);
        mTextPaint.setAlpha(alpha);
        canvas.drawCircle(centerX - imageButton.getWidth()/2 + radiusCircle, centerY + radiusCircle, animationPetals() ,mTextPaint); //gauche
        mTextPaint.setColor(Color.GREEN);
        mTextPaint.setAlpha(alpha);
        canvas.drawCircle(centerX + imageButton.getWidth()/2 + radiusCircle, centerY + radiusCircle, animationPetals() ,mTextPaint);//droite
        mTextPaint.setColor(Color.YELLOW);
        mTextPaint.setAlpha(alpha);
        canvas.drawCircle(centerX + radiusCircle , centerY - imageButton.getHeight()/2 + radiusCircle, animationPetals() ,mTextPaint);//haut
        mTextPaint.setColor(Color.BLUE);
        mTextPaint.setAlpha(alpha);
        canvas.drawCircle(centerX + radiusCircle , centerY + imageButton.getHeight()/2 + radiusCircle, animationPetals() ,mTextPaint); //bas
    }

    public void drawText(Canvas canvas, float centerX, float centerY)
    {
        mTextPaint.setColor(Color.BLACK);
        mTextPaint.setTextSize(tailleFont);
        mTextPaint.setAntiAlias(true);
        mTextPaint.setTextAlign(Paint.Align.CENTER);
        int translation = 20;

        //Text droit vertical
        Path path = new Path();
        path.moveTo(centerX - imageButton.getWidth()/2 + radiusCircle - translation, this.getHeight());
        path.lineTo(centerX - imageButton.getWidth()/2 + radiusCircle - translation, 0);
        canvas.drawTextOnPath(Actions.Image.toString(), path, 0, 10, mTextPaint);
        //Texte gauche vertical
        Path path2 = new Path();path2.toggleInverseFillType();
        path2.moveTo(centerX + imageButton.getWidth()/2 + radiusCircle + translation, this.getHeight());
        path2.lineTo(centerX + imageButton.getWidth()/2 + radiusCircle + translation, 0);

        canvas.drawText(Actions.Text.toString(), centerX +radiusCircle , centerY - imageButton.getHeight()/2 + radiusCircle - translation , mTextPaint);//haut

        //Texte gauche Rotation 180°
        Matrix matrix = new Matrix();
        matrix.setScale(-1, -1, this.getWidth()/2, this.getHeight()/2);
        path2.transform(matrix);
        canvas.drawTextOnPath(Actions.Son.toString(), path2, 0, -230, mTextPaint);

        canvas.drawText(Actions.Zoom.toString(), centerX + radiusCircle , centerY + imageButton.getHeight()/2 + radiusCircle + translation, mTextPaint);//bas
    }

    //Painting
    public float animationPetals()
    {
        if(radius <= radiusCircle) {
            radius = radius + speedGrowth;
            this.invalidate();
        }
        return radius;
    }

    @Override
    protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {
        int parentWidth = MeasureSpec.getSize(widthMeasureSpec);
        int parentHeight = MeasureSpec.getSize(heightMeasureSpec);


        this.setMeasuredDimension(parentWidth, parentHeight );
    }
}
