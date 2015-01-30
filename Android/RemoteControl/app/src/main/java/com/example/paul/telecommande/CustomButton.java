package com.example.paul.telecommande;

import android.animation.ObjectAnimator;
import android.app.Fragment;
import android.app.Notification;
import android.content.Context;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Point;
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

    private Paint mTextPaint = new Paint();;
    private Bitmap bitmap;
    private int number;

    private boolean pressLong = false;

    private int res;

    public CustomButton(Context context, int img, boolean resize, int n) {
        super(context);
        res = img;
        number = n;
        decoupeImage(resize);



        this.setBackgroundColor(Color.YELLOW);

        this.setOnLongClickListener(speakHoldListener);
        this.setOnTouchListener(speakTouchListener);
    }

    public Bitmap getBitmap()
    {
        return bitmap;
    }



    public void decoupeImage(boolean needToResize)
    {
        BitmapFactory.Options options = new BitmapFactory.Options();
        options.inSampleSize = 8;
        bitmap = BitmapFactory.decodeResource(getResources(),res, options);

        if(needToResize) {
            bitmap = getRound();
        }
    }

    @Override
    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);

        //centre de l'image
        float centerX = this.getWidth()/2 - bitmap.getWidth()/2;
        float centerY = this.getHeight()/2 - bitmap.getHeight()/2;
        //largueur et longueur de la vue avec l'image
        float heightImg = bitmap.getHeight();
        float widthImg = bitmap.getWidth();

        canvas.drawBitmap(bitmap, centerX, centerY , mTextPaint);

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
        int parentWidth = MeasureSpec.getSize(widthMeasureSpec);
        int parentHeight = MeasureSpec.getSize(heightMeasureSpec);

        this.setMeasuredDimension(bitmap.getWidth(), bitmap.getHeight());

        this.setX(((number * parentWidth)/(number+1)) - bitmap.getWidth()/2);
        this.setY(((number * parentHeight)/(number+1)) - bitmap.getHeight()/2);
    }

//Listener
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
}
