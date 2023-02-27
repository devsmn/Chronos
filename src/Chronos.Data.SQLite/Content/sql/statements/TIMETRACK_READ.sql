SELECT tt.TT_ref as Key,
	   tt.TT_startTime as StartTime,
	   tt.TT_endTime as EndTime
  FROM TIMETRACK tt 
 WHERE CASE WHEN @TT_ref = -1 THEN tt.TT_Ref = tt.TT_Ref ELSE tt.TT_ref = @TT_ref END;



