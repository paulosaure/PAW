package com.example.paul.telecommande;

import android.content.Context;
import android.util.Log;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.ViewConfiguration;

/**
 * Created by Paul on 02/02/2015.
 */
public class MyGestureDetector implements GestureDetector.OnGestureListener {

    private  int minSwipeDelta;
    private  int minSwipeVelocity;
    private  int maxSwipeVelocity;

    public MyGestureDetector(Context ctx)
    {
        Log.e("tets","testt");
        minSwipeDelta = ViewConfiguration.get(ctx).getScaledPagingTouchSlop();
        minSwipeVelocity = ViewConfiguration.get(ctx).getScaledMinimumFlingVelocity();
        maxSwipeVelocity = ViewConfiguration.get(ctx).getScaledMaximumFlingVelocity();
    }

    @Override
    public boolean onDown(MotionEvent e) {
        return false;
    }

    @Override
    public void onShowPress(MotionEvent e) {

    }

    @Override
    public boolean onSingleTapUp(MotionEvent e) {
        return false;
    }

    @Override
    public boolean onScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY) {
        return false;
    }

    @Override
    public void onLongPress(MotionEvent e) {

    }

    @Override
    public boolean onFling(MotionEvent event1, MotionEvent event2, float velocityX, float velocityY) {
        Log.e("deltaX", " : " + event1.getX() + " and : " + event2.getX());
        Log.e("deltaY", " : " + event1.getY() + " and : " + event2.getY());

        boolean result = false;
        try {
            float deltaX = event2.getX() - event1.getX();
            float deltaY = event2.getY() - event1.getY();
            float absVelocityX = Math.abs(velocityX);
            float absVelocityY = Math.abs(velocityY);
            float absDeltaX = Math.abs(deltaX);
            float absDeltaY = Math.abs(deltaY);
            Log.e("velotcity X", " : " + absVelocityX + " absDeltaX : " + absDeltaX);
            if (absDeltaX > absDeltaY) {
                if (absDeltaX > minSwipeDelta && absVelocityX > minSwipeVelocity
                        && absVelocityX < maxSwipeVelocity) {
                    if (deltaX < 0) {
                        onSwipeLeft();
                    } else {
                        onSwipeRight();
                    }
                }
                result = true;
            } else if (absDeltaY > minSwipeDelta && absVelocityY > minSwipeVelocity
                    && absVelocityY < maxSwipeVelocity) {
                if (deltaY < 0) {
                    onSwipeTop();
                } else {
                    onSwipeBottom();
                }
            }
            result = true;
        } catch (Exception e) {
            e.printStackTrace();
        }
        return result;
    }

    public void onSwipeLeft() {
        Log.e("test", "LEFT");
    }

    public void onSwipeRight() {
        Log.e("test", "RIGHT");}

    public void onSwipeTop() {
        Log.e("test", "TOP");}

    public void onSwipeBottom() {
        Log.e("test", "BOTTOM");}
}
