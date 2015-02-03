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
import android.support.v4.view.GestureDetectorCompat;
import android.util.AttributeSet;
import android.util.Log;
import android.view.GestureDetector;
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
public class CustomButton extends ImageView implements OnGestureListener {

    private Paint mTextPaint = new Paint();
    private GestureDetector gDetector;

    private Bitmap bitmap ;
    {
        BitmapFactory.Options options = new BitmapFactory.Options();
        options.inSampleSize = 8;
        bitmap = BitmapFactory.decodeResource(getResources(),R.drawable.touchme, options);
    }

    private int number;


    private int res;

    public CustomButton(Context context, AttributeSet set) {
        super(context, set);
        gDetector = new GestureDetector(this);
    }


    public CustomButton(Context context, int img, boolean resize, int n) {
        super(context);
        res = img;
        number = n;
        decoupeImage(resize);
        //setBackgroundColor(Color.YELLOW);
        gDetector = new GestureDetector(this);
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
            //bitmap = getRound();
        }
    }

    public void deplacementButton()
    {

    }

    @Override
    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);

        float centerX = this.getWidth()/2 - bitmap.getWidth()/2;
        float centerY = this.getHeight()/2 - bitmap.getHeight()/2;

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
        super.onMeasure(widthMeasureSpec, heightMeasureSpec);

        if (bitmap != null)  {
            this.setMeasuredDimension(bitmap.getWidth(), bitmap.getHeight());
        }
    }
}
