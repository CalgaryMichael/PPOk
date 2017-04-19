UPDATE message_hisory
SET prescription_id = @prescription_id, response = @response,
	fill_date = @fill_date, pick_up_date = @pick_up_date, filled = @filled
WHERE message_id = @message_id