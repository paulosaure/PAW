package com.example.paul.telecommande;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Point;
import android.graphics.Rect;
import android.view.View;

/**
 * Created by Paul on 30/01/2015.
 */
public class Petals extends View {
        Paint paint = new Paint();
        private int number;

        float radiusCircle = 80;
        float radius = 0;
        Bitmap imageButton;

        public Petals(Context context, Actions act, Bitmap img, int n) {
            super(context);
            number = n;
            imageButton = img;
        }

        @Override
        public void onDraw(Canvas canvas) {

                switch (number)
                {
                    case 1 :
                        canvas.drawCircle(centerX - widthImg/2 - radiusCircle - 10 + radiusCircle , centerY + radiusCircle, drawPetals() ,mTextPaint);//gauche
                        break;
                    case 2 :
                        canvas.drawCircle(centerX  + widthImg/2 + radiusCircle + 10 + radiusCircle, centerY + radiusCircle, drawPetals() ,mTextPaint);//droite
                        break;
                    case 3 :
                        canvas.drawCircle(centerX + radiusCircle, centerY - heightImg/2 - radiusCircle - 10 + radiusCircle, drawPetals() ,mTextPaint);//haut
                       break;
                    case 4 :
                        canvas.drawCircle(centerX + radiusCircle, centerY + heightImg/2 + radiusCircle + 10 + radiusCircle , drawPetals() ,mTextPaint);//bas
                    break;
                }

        }

    //Painting
    public float drawPetals()
    {
        if(radius <= radiusCircle) {
            radius = radius + 3;
        }

        this.invalidate();
        return radius;
    }

}
