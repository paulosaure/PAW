package com.example.paul.telecommande;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.PorterDuff;
import android.graphics.PorterDuffXfermode;
import android.graphics.Rect;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.shapes.OvalShape;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;

import com.example.paul.remotecontrol.R;

/**
 * Created by Paul on 29/01/2015.
 */
public class CustomButton extends ImageView {

    Actions actions[];
    Drawable imageButton;
    Paint mTextPaint;



    public CustomButton(Context context, Actions acts[], int img ) {
        super(context);
        initPaint();
        imageButton = context.getResources().getDrawable(img);
        actions = acts;
        setListener();
    }

    @Override
    protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {

        int parentWidth = MeasureSpec.getSize(widthMeasureSpec);
        int parentHeight = MeasureSpec.getSize(heightMeasureSpec);

        this.setMeasuredDimension(parentWidth, parentHeight);
    }

    private void initPaint() {
        mTextPaint = new Paint();

    }

    @Override
    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);


        Rect imageBounds = canvas.getClipBounds();  // Adjust this for where you want it

        imageButton.setBounds(imageBounds);
        imageButton.draw(canvas);


    }

    /*    Bitmap b = BitmapFactory.decodeResource(getResources(), R.drawable.touchme);
        mTextPaint.setColor(Color.RED);
        canvas.drawBitmap(b, 0, 0, mTextPaint);
*/

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
