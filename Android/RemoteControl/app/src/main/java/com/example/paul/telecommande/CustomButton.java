package com.example.paul.telecommande;

import android.animation.ObjectAnimator;
import android.app.Notification;
import android.content.Context;
import android.content.res.Resources;
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
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;

import com.example.paul.remotecontrol.R;

import java.util.ArrayList;

/**
 * Created by Paul on 29/01/2015.
 */
public class CustomButton extends ImageView {

    private Actions actions[];
    private Paint mTextPaint;
    private Bitmap bitmap;

    private int height;
    private int width;

    private boolean pressLong = false;

    float radius = 0;

    public CustomButton(Context context, Actions acts[], int img ) {
        super(context);

        mTextPaint = new Paint();

        BitmapFactory.Options options = new BitmapFactory.Options();
        options.inSampleSize = 8;

        bitmap = BitmapFactory.decodeResource(getResources(),img, options);
        actions = acts;
        this.setOnLongClickListener(speakHoldListener);
        this.setOnTouchListener(speakTouchListener);
    }

    private View.OnLongClickListener speakHoldListener = new View.OnLongClickListener() {
            @Override
            public boolean onLongClick(View v) {
                pressLong = true;
                radius = 0;
                drawPetals();
                return true;
            }
    };

    private View.OnTouchListener speakTouchListener = new View.OnTouchListener() {
        @Override
        public boolean onTouch(View pView, MotionEvent pEvent) {
            pView.onTouchEvent(pEvent);
            // We're only interested in when the button is released.
            if (pEvent.getAction() == MotionEvent.ACTION_UP) {
                // We're only interested in anything if our speak button is currently pressed.
                if (pressLong) {
                    // Do something when the button is released.
                    pressLong = false;
                    drawPetals();
                }
            }
            return false;
        }
    };

    public float drawPetals()
    {
        if(radius <= 80) {
            radius = radius + 3;
        }
        
        this.invalidate();
        return radius;
    }


    @Override
    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        int positionWidth =  width/4;
        int positionHeight =  height/4;
        bitmap = getRound();
        canvas.drawBitmap(bitmap, positionWidth, positionHeight, mTextPaint);

        if(pressLong)
        {
            canvas.drawCircle(positionWidth , positionHeight, drawPetals() ,mTextPaint);
        }
    }

    public Bitmap getRound() {
        Bitmap output = Bitmap.createBitmap(bitmap.getWidth(),
                bitmap.getHeight(), Bitmap.Config.ARGB_8888);
        Canvas canvas = new Canvas(output);
        final int color = 0xff424242;
        final Paint paint = new Paint();
        final Rect rect = new Rect(0, 0, bitmap.getWidth(),
                bitmap.getHeight());

        paint.setAntiAlias(true);
        canvas.drawARGB(0, 0, 0, 0);
        // paint.setColor(color);
        canvas.drawCircle(bitmap.getWidth() / 2,
                bitmap.getHeight() / 2, bitmap.getWidth() / 2, paint);
        paint.setXfermode(new PorterDuffXfermode(PorterDuff.Mode.SRC_IN));
        canvas.drawBitmap(bitmap, rect, rect, paint);
        return output;
    }

    @Override
    protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {

        width = MeasureSpec.getSize(widthMeasureSpec);
        height = MeasureSpec.getSize(heightMeasureSpec);

        this.setMeasuredDimension(width, height);
    }
}
