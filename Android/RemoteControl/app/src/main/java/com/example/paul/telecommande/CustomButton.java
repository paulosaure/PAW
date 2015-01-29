package com.example.paul.telecommande;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.util.Log;
import android.view.View;

/**
 * Created by Paul on 29/01/2015.
 */
public class CustomButton extends View {

    Actions actions[];
    String imageButton;
    Paint mTextPaint;



    public CustomButton(Context context, Actions acts[], String img ) {
        super(context);
        initPaint();
        //setWillNotDraw(false);
        //this.invalidate();

        actions = acts;
        imageButton = img;
        setListener();

        Log.e("test", "constructeur");

    }

    @Override
    protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {

        setMeasuredDimension(400, 400);
    }


    @Override
    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);

        Log.e("test", "DRAAAAAAAW");
        canvas.drawCircle(400, 400, 40, mTextPaint);
    }

    private void initPaint() {
        mTextPaint = new Paint();

    }


    public void setListener()
    {
        this.setOnLongClickListener(new View.OnLongClickListener() {
            @Override
            public boolean onLongClick(View v) {
                Log.e("test", "touche");
                displayPetals();
                return true;
            }
        });
    }

    public void displayPetals()
    {

    }
}
